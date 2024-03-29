﻿namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Categories;
    using Data;
    using Data.Entities;

    /// <summary>
    /// Handling all category logic.
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext context;

        public CategoryService(AppDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Get category by its Id.
        /// </summary>
        /// <param name="id">The Id of the searched category.</param>
        /// <returns></returns>
        public async Task<Category?> GetAsync(int id)
        {
            return await context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Get the names of all the categories in the database.
        /// </summary>
        /// <returns>Names of all categories.</returns>
        public async Task<IEnumerable<string>> GetAllNamesAsync()
        {
            return await context.Categories
                .Where(c => c.IsDeleted == false)
                .Select(c => c.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            var entities = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial,
                    IsDeleted = c.IsDeleted
                    
                }).ToListAsync();

            return entities;
        }

        /// <summary>
        /// Gets all available categories from the database.
        /// </summary>
        /// <returns>List of categories where IsDeleted == false.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllAvailableAsync()
        {
            var entities = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial,
                    IsDeleted = c.IsDeleted

                }).ToListAsync();

            return entities.Where(e => e.IsDeleted == false);
        }

        /// <summary>
        /// Get all of the official categories from the database.
        /// </summary>
        /// <returns>A list containing all of the official categories that are currently added to the database.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetOfficialAsync()
        {
            return await context.Categories
                .Where(c => c.IsOfficial)
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial,
                    IsDeleted = c.IsDeleted
                }).ToListAsync();
        }

        /// <summary>
        /// Add a category to the database.
        /// Only admins can add categories to the database.
        /// </summary>
        /// <param name="model"></param>
        public async Task AddAsync(CategoryFormViewModel model)
        {
            var entity = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                IsOfficial = false,
                IsDeleted = false
            };

            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit a category from the database.
        /// </summary>
        /// <param name="model"></param>
        public async Task EditAsync(CategoryFormViewModel model)
        {
            var entity = await context.Categories
                .FirstOrDefaultAsync(c => c.Id == model.Id);

            if (entity != null)
            {
                entity.Name = model.Name;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Toggles the IsOfficial property for the selected category.
        /// </summary>
        /// <param name="id">Id of the category that should be verified.</param>
        /// <returns></returns>
        public async Task VerifyAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                entity.IsOfficial = entity.IsOfficial == true
                    ? false
                    : true;

                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a category from the list.
        /// </summary>
        /// <param name="id">Id of the category that should be deleted.</param>
        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Restore a category to the list.
        /// </summary>
        /// <param name="id">Id of the category that should be restored.</param>
        public async Task RestoreAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                await context.SaveChangesAsync();
            }
        }
    }
}
