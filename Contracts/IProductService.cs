namespace techIE.Contracts
{
    using Data.Entities;

    /// <summary>
    /// Handling all product logic.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Get all of the official categories from the database.
        /// </summary>
        /// <returns>A list containing all of the official categories that are currently added to the database.</returns>
        Task<IEnumerable<Category>> GetOfficialCategoriesAsync();
    }
}
