namespace techIE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Products;
    using Data;
    using Data.Entities;
    using Data.Entities.Enums;

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
        /// Get a product with provided Id.
        /// </summary>
        /// <param name="id">Id of requested product.</param>
        /// <returns>Product with matching Id.</returns>
        public async Task<Product?> GetAsync(int id)
        {
            return await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
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
        /// Add a category to the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        public async Task AddAsync(ProductFormViewModel model)
        {
            var entity = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Color = (Color)model.Color,
                Description = model.Description,
                IsOfficial = true,
                CategoryId = model.CategoryId
            };

            await context.Products.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit a product from the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        public async Task EditAsync(ProductFormViewModel model)
        {
            var entity = await context.Products
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Price = model.Price;
                entity.ImageUrl = model.ImageUrl;
                entity.Color = model.Color;
                entity.Description = model.Description;
                entity.CategoryId = model.CategoryId;
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Delete a product from the database;
        /// </summary>
        /// <param name="id">Id of deleted product.</param>
        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                context.Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
