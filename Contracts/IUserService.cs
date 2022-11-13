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
        /// Checks if the current user is an admin or not.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Value of the bool user property IsAdmin.</returns>
        Task<bool> IsAdminAsync(string userId);
    }
}
