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
        /// Get all of the official categories from the database.
        /// </summary>
        /// <returns>A list containing all of the official categories that are currently added to the database.</returns>
        Task<IEnumerable<CategoryViewModel>> GetOfficialCategoriesAsync();
    }
}
