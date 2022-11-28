namespace techIE.Contracts
{
    using Models.Products;
    using Data.Entities;

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
        /// Get all products with IsOfficial == true.
        /// Main use is in the admin product panel.
        /// </summary>
        /// <returns>List of official products for admin panel.</returns>
        Task<IEnumerable<ProductAdminPanelViewModel>> GetAllAdminAsync();

        /// <summary>
        /// Get all product with IsOfficial == true.
        /// Main use is in the official store page.
        /// </summary>
        /// <returns>List of official products for store.</returns>
        Task<IEnumerable<ProductOverviewViewModel>> GetAllOfficialAsync();

        /// <summary>
        /// Get three random products.
        /// They are displayed on the store index pages.
        /// </summary>
        /// <param name="isOfficial">Checks if the returned products should be official or not.</param>
        /// <returns>Three random products.</returns>
        Task<IEnumerable<ProductOverviewViewModel>> GetThreeRandomAsync(bool isOfficial);

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
