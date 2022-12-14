namespace techIE.Contracts
{
    using Models;

    /// <summary>
    /// Handling some of the user related logic.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get current user by using their ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User with provided ID.</returns>
        Task<UserViewModel?> GetUserAsync(string userId);

        /// <summary>
        /// Get user by product Id.
        /// Method used to display who has listed the product.
        /// </summary>
        /// <param name="productId">Id of the product whose user we want to find.</param>
        /// <returns>User if successful. Otherwise, null.</returns>
        Task<UserViewModel?> GetUserByProductIdAsync(int productId);
    }
}
