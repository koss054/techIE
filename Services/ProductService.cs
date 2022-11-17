namespace techIE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Products;
    using Models.Categories;
    using Data;

    /// <summary>
    /// Handling all product logic.
    /// </summary>
    public class ProductService : IProductService
    {
        private readonly AppDbContext context;

        public ProductService(AppDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Get all products with IsOfficial == true.
        /// Main use is in the admin product panel.
        /// </summary>
        /// <returns>List of official products.</returns>
        public async Task<IEnumerable<ProductAdminPanelViewModel>> GetAllOfficialAsync()
        {
            return await context.Products
                .Where(p => p.IsOfficial)
                .Select(p => new ProductAdminPanelViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = p.Category.Name,
                    IsOfficial = p.IsOfficial
                }).ToListAsync();
        }

        /// <summary>
        /// Get all of the official categories from the database.
        /// </summary>
        /// <returns>A list containing all of the official categories that are currently added to the database.</returns>
        public async Task<IEnumerable<CategoryViewModel>> GetOfficialCategoriesAsync()
        {
            return await context.Categories
                .Where(c => c.IsOfficial)
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    IsOfficial = c.IsOfficial
                }).ToListAsync();
        }
    }
}
