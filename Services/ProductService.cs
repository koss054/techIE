namespace techIE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models.Products;
    using Services.Products;
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
        public async Task<IEnumerable<ProductAdminPanelViewModel>> GetAllAdminAsync()
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
        /// Get all products from database.
        /// </summary>
        /// <param name="isOfficial">Checks if the returned products should be official or not.</param>
        /// <returns>List of products for both stores.</returns>
        public async Task<IEnumerable<ProductOverviewViewModel>> GetAllAsync(bool isOfficial)
        {
            return await context.Products
                .Where(p => p.IsOfficial)
                .Select(p => new ProductOverviewViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                }).ToListAsync();
        }

        /// <summary>
        /// Get three random products.
        /// They are displayed on the store index pages.
        /// </summary>
        /// <param name="isOfficial">Checks if the returned products should be official or not.</param>
        /// <returns>Three random products.</returns>
        public async Task<IEnumerable<ProductOverviewViewModel>> GetThreeRandomAsync(bool isOfficial)
        {
            return await context.Products
                .Where(p => p.IsOfficial == isOfficial)
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .Select(p => new ProductOverviewViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                }).ToListAsync();
        }

        /// <summary>
        /// Gets products for the Explore page in the user marketplace.
        /// </summary>
        /// <param name="category">Searched category.</param>
        /// <param name="searchTerm">Term contained in one of the searched products.</param>
        /// <param name="sorting">Sorting method for products.</param>
        /// <param name="currentPage">Page that the user is on.</param>
        /// <param name="productsPerPage">Products that are visualized per page.</param>
        /// <returns></returns>
        public async Task<ProductQueryServiceModel> GetSearchResultAsync(
            string? category = null,
            string? searchTerm = null,
            ProductSorting sorting = ProductSorting.Newest,
            int currentPage = 1,
            int productsPerPage = 1)
        {
            var productQuery = context.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category))
            {
                productQuery = context.Products
                    .Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = context.Products.Where(p =>
                    p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            productQuery = sorting switch
            {
                ProductSorting.Newest => productQuery.OrderBy(p => p.Id),
                ProductSorting.Price => productQuery.OrderBy(p => p.Price),
                _ => productQuery.OrderBy(p => p.Name)
            };

            var products = await productQuery
                .Skip((currentPage - 1) * productsPerPage)
                .Take(productsPerPage)
                .Select(p => new ProductOverviewViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name
                }).ToListAsync();

            var totalProducts = productQuery.Count();
            return new ProductQueryServiceModel()
            {
                TotalProductCount = totalProducts,
                Products = products
            };
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
                IsOfficial = model.IsOfficial,
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
