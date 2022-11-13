namespace techIE.Contracts
{
    using Models.Categories;

    /// <summary>
    /// Handling all category logic.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();

        /// <summary>
        /// Add a category to the database.
        /// </summary>
        /// <param name="model"></param>
        Task AddAsync(AddCategoryViewModel model);
    }
}
