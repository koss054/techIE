namespace techIE.Services
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
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        public async Task<AdminCategoryViewModel> GetAllAsync()
        {
            var entities = await context.Categories
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial
                }).ToListAsync();

            var adminEntity = new AdminCategoryViewModel()
            {
                Categories = entities,
                AddedCategory = new AddCategoryViewModel()
                {
                    IsOfficial = false
                }
            };

            return adminEntity;
        }

        /// <summary>
        /// Add a category to the database.
        /// Only admins can add categories to the database, so IsOfficial is always true.
        /// </summary>
        /// <param name="model"></param>
        public async Task AddAsync(AddCategoryViewModel model)
        {
            var entity = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                IsOfficial = true
            };

            await context.Categories.AddAsync(entity);
            await context.SaveChangesAsync();
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
    }
}
