namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Entities;
    using Models.Categories;

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
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
        {
            return await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial
                }).ToListAsync();
        }

        /// <summary>
        /// Add a category to the database.
        /// </summary>
        /// <param name="model"></param>
        public async Task AddAsync(AddCategoryViewModel model)
        {
            var entity = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                IsOfficial = model.IsOfficial
            };

            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
        }
    }
}
