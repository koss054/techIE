namespace techIE.Contracts
{
    using Models.Categories;
    using Data.Entities;

    /// <summary>
    /// Handling all category logic.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Get category by its Id.
        /// </summary>
        /// <param name="id">The Id of the searched category.</param>
        /// <returns></returns>
        Task<Category?> GetAsync(int id);

        /// <summary>
        /// Get the names of all the categories in the database.
        /// </summary>
        /// <returns>Names of all categories.</returns>
        Task<IEnumerable<string>> GetAllNamesAsync();

        /// <summary>
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        Task<IEnumerable<CategoryViewModel>> GetAllAsync();

        /// <summary>
        /// Get all of the official categories from the database.
        /// </summary>
        /// <returns>A list containing all of the official categories that are currently added to the database.</returns>
        Task<IEnumerable<CategoryViewModel>> GetOfficialAsync();

        /// <summary>
        /// Add a category to the database.
        /// </summary>
        /// <param name="model"></param>
        Task AddAsync(CategoryFormViewModel model);

        /// <summary>
        /// Edit a category from the database
        /// </summary>
        /// <param name="model"></param>
        Task EditAsync(CategoryFormViewModel model);

        /// <summary>
        /// Toggles the IsOfficial property for the selected category.
        /// </summary>
        /// <param name="id">Id of the category that should be verified.</param>
        Task VerifyAsync(int id);

        /// <summary>
        /// Delete a category from the list.
        /// </summary>
        /// <param name="id">Id of the category that should be deleted.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
