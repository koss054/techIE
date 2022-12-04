namespace techIE.Contracts
{
    using Models.Products;
    using Services.Products;
    using Data.Entities;
    using Data.Entities.Enums;

    /// <summary>
    /// Handling all product logic.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get a product with provided Id.
        /// </summary>
        /// <param name="id">Id of requested product.</param>
        /// <returns>Product with matching Id.</returns>
        Task<Product?> GetAsync(int id);

        /// <summary>
        /// Get product with more details.
        /// Used in product detail pages.
        /// </summary>
        /// <param name="id">Id of the product that we are looking for.</param>
        /// <returns>Product with additional details.</returns>
        Task<ProductDetailedViewModel?> GetDetailedAsync(int id);

        /// <summary>
        /// Get all products with IsOfficial == true.
        /// Main use is in the admin product panel.
        /// </summary>
        /// <returns>List of official products for admin panel.</returns>
        Task<IEnumerable<ProductAdminPanelViewModel>> GetAllAdminAsync();

        /// <summary>
        /// Get all product with IsOfficial == true.
        /// </summary>
        /// <param name="isOfficial">Checks if the returned products should be official or not.</param>
        /// <returns>List of products for both stores.</returns>
        Task<IEnumerable<ProductOverviewViewModel>> GetAllAsync(bool isOfficial);

        /// <summary>
        /// Get three random products.
        /// They are displayed on the store index pages.
        /// </summary>
        /// <param name="isOfficial">Checks if the returned products should be official or not.</param>
        /// <returns>Three random products.</returns>
        Task<IEnumerable<ProductOverviewViewModel>> GetThreeRandomAsync(bool isOfficial);

        /// <summary>
        /// Gets products for the Explore page in the user marketplace.
        /// </summary>
        /// <param name="category">Searched category.</param>
        /// <param name="searchTerm">Term contained in one of the searched products.</param>
        /// <param name="sorting">Sorting method for products.</param>
        /// <param name="currentPage">Page that the user is on.</param>
        /// <param name="productsPerPage">Products that are visualized per page.</param>
        /// <returns></returns>
        Task<ProductQueryServiceModel> GetSearchResultAsync(
            string? category = null,
            string? searchTerm = null,
            ProductSorting sorting = ProductSorting.Newest,
            int currentPage = 1,
            int productsPerPage = 1);

        /// <summary>
        /// Add a product to the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        Task AddAsync(ProductFormViewModel model);

        /// <summary>
        /// Edit a product from the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        Task EditAsync(ProductFormViewModel model);

        /// <summary>
        /// Delete a product from the database;
        /// </summary>
        /// <param name="id">Id of deleted product.</param>
        Task DeleteAsync(int id);
    }
}
