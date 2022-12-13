namespace techIE.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Contracts;

    using Models;
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

        #region GetProduct
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
        /// Get product with more details.
        /// Used in product detail pages.
        /// </summary>
        /// <param name="id">Id of the product that we are looking for.</param>
        /// <returns>Product with additional details.</returns>
        public async Task<ProductDetailedViewModel?> GetDetailedAsync(int id, UserViewModel seller)
        {
            return await context.Products
                .Where(p => p.Id == id && p.IsDeleted == false)
                .Select(p => new ProductDetailedViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    Color = p.Color,
                    Description = p.Description,
                    IsOfficial = p.IsOfficial,
                    IsDeleted = p.IsDeleted,
                    Seller = seller
                }).FirstOrDefaultAsync();
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
                    IsOfficial = p.IsOfficial,
                    IsDeleted = p.IsDeleted
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
                .Where(p => p.IsOfficial && p.IsDeleted == false)
                .Select(p => new ProductOverviewViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsDeleted = p.IsDeleted
                }).ToListAsync();
        }

        /// <summary>
        /// Get product form model, used when user wants to edit.
        /// </summary>
        /// <param name="id">Id of the product that is going to be edited.</param>
        /// <returns>ProductFormViewModel of a product with the provided Id.</returns>
        public async Task<ProductFormViewModel?> GetFormModelAsync(int id)
        {
            return await context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductFormViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Color = p.Color,
                    Description = p.Description,
                    IsOfficial = p.IsOfficial,
                    CategoryId = p.CategoryId,
                }).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Get the products listed by the provided user.
        /// Gets both available and deleted products.
        /// </summary>
        /// <param name="userId">Id of the user who is trying to view their products.</param>
        /// <returns>List of products added by the provided user.</returns>
        public async Task<IEnumerable<ProductOverviewViewModel>> GetCurrentUserProductsAsync(string userId)
        {
            return await context.UsersProducts
                .Where(up => up.UserId == userId)
                .Include(p => p.Product)
                .Select(up => new ProductOverviewViewModel()
                {
                    Id = up.Product.Id,
                    Name = up.Product.Name,
                    Price = up.Product.Price,
                    ImageUrl = up.Product.ImageUrl,
                    Category = up.Product.Category.Name,
                    IsDeleted = up.Product.IsDeleted
                })
                .ToListAsync();
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
                .Where(p => p.IsOfficial == isOfficial && p.IsDeleted == false)
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .Select(p => new ProductOverviewViewModel()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsDeleted = p.IsDeleted
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
            // We only take the unofficial products as the search bar is only used in the Marketplace section.
            var productQuery = context.Products
                .Where(p => p.IsOfficial == false &&
                            p.IsDeleted == false)
                .AsQueryable();

            // Made the if statement a lot bigger than necessary because the search term was overriding the category.
            // Will try to reduce the lines of code when I'm refactoring the app, if I have time D:
            if (!string.IsNullOrWhiteSpace(category) && !string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = context.Products.Where(p =>
                    (p.Category.Name == category &&
                    (p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower()))) &&
                    p.IsOfficial == false && p.IsDeleted == false);
            }
            else if (!string.IsNullOrWhiteSpace(category))
            {
                productQuery = context.Products.Where(p => 
                    p.Category.Name == category &&
                    p.IsOfficial == false && p.IsDeleted == false);
            }
            else if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productQuery = context.Products.Where(p =>
                    (p.Name.ToLower().Contains(searchTerm.ToLower()) ||
                    p.Description.ToLower().Contains(searchTerm.ToLower())) &&
                    p.IsOfficial == false && p.IsDeleted == false);
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
                .Distinct()
                .Select(p => new ProductOverviewViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    Category = p.Category.Name,
                    IsDeleted = p.IsDeleted
                }).ToListAsync();

            var totalProducts = productQuery.Count();
            return new ProductQueryServiceModel()
            {
                TotalProductCount = totalProducts,
                Products = products
            };
        }
        #endregion

        #region AddProduct
        /// <summary>
        /// Add a product to the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        /// <param name="userId">User adding the product.</param>
        public async Task AddAsync(ProductFormViewModel model, string userId)
        {
            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                Color = (Color)model.Color,
                Description = model.Description,
                IsOfficial = model.IsOfficial,
                IsDeleted = false,
                CategoryId = model.CategoryId
            };

            await context.Products.AddAsync(product);
            await this.AddToUserProduct(product, userId);
            await context.SaveChangesAsync();
        }
        #endregion

        #region EditProduct
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
        #endregion

        #region DeleteProduct
        /// <summary>
        /// Delete a product, making it unavailable in the stores.
        /// </summary>
        /// <param name="id">Id of the deleted product.</param>
        public async Task DeleteAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region RestoreProduct
        /// <summary>
        /// Restora a deleted product.
        /// </summary>
        /// <param name="id">Id of the restored product.</param>
        public async Task RestoreAsync(int id)
        {
            var entity = await this.GetAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = false;
                await context.SaveChangesAsync();
            }
        }
        #endregion

        #region UserValidation
        /// <summary>
        /// Checks if the provided user is the seller of the product.
        /// </summary>
        /// <param name="productId">Id of product which we are checking.</param>
        /// <param name="userId">User who we are validating.</param>
        /// <returns>True if the user sells the product. False if they don't.</returns>
        public async Task<bool> IsUserSellerAsync(int productId, string userId)
        {
            var userProduct = await context.UsersProducts
                .FirstOrDefaultAsync(up => up.ProductId == productId &&
                                           up.UserId == userId);

            return userProduct != null ? true : false;
        }
        #endregion

        #region PrivateMethods
        /// <summary>
        /// Adding the product to the mapping table.
        /// This way the product and the user are tied together.
        /// </summary>
        /// <param name="product">Product that is being added to the database.</param>
        /// <param name="userId">User that is adding the product.</param>
        private async Task AddToUserProduct(Product product, string userId)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user != null)
            {
                user.UsersProducts.Add(new UserProduct()
                {
                    UserId = userId,
                    User = user,
                    ProductId = product.Id,
                    Product = product
                });
            }
            await context.SaveChangesAsync();
        }
        #endregion
    }
}
