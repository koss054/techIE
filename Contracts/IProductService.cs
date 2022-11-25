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
        /// <returns>List of official products.</returns>
        Task<IEnumerable<ProductAdminPanelViewModel>> GetAllOfficialAsync();

        /// <summary>
        /// Get three random products that are official.
        /// They are displayed on the official store index page.
        /// </summary>
        /// <returns>Three random official products.</returns>
        Task<IEnumerable<ProductOverviewViewModel>> GetThreeRandomOfficialAsync();

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
