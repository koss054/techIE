namespace techIE.Contracts
{
    using Models.Products;
    using Models.Categories;

    /// <summary>
    /// Handling all product logic.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all products with IsOfficial == true.
        /// Main use is in the admin product panel.
        /// </summary>
        /// <returns>List of official products.</returns>
        Task<IEnumerable<ProductAdminPanelViewModel>> GetAllOfficialAsync();

        /// <summary>
        /// Add a category to the database.
        /// </summary>
        /// <param name="model">Model with validation.</param>
        Task AddAsync(AddProductViewModel model);
    }
}
