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
        /// Gets all categories from the database.
        /// </summary>
        /// <returns>List of categories.</returns>
        Task<AdminCategoryViewModel> GetAllAsync();

        /// <summary>
        /// Add a category to the database.
        /// </summary>
        /// <param name="model"></param>
        Task AddAsync(AddCategoryViewModel model);

        /// <summary>
        /// Toggles the IsOfficial property for the selected category.
        /// </summary>
        /// <param name="id">Id of the category that should be verified.</param>
        /// <returns></returns>
        Task VerifyAsync(int id);

        /// <summary>
        /// Delete a category from the list.
        /// </summary>
        /// <param name="id">Id of the category that should be deleted.</param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
