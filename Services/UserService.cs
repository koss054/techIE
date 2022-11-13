namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Models;
    using Data;

    /// <summary>
    /// Handling some of the user related logic.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly AppDbContext context;

        public UserService(AppDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Get current user by using their ID.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>User with provided ID.</returns>
        public async Task<UserViewModel?> GetUserAsync(string userId)
        {
            return await context.Users
                .Select(u => new UserViewModel()
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsAdmin = u.IsAdmin
                }).FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Checks if the current user is an admin or not.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Value of the bool user property IsAdmin.</returns>
        public bool IsAdminAsync(string userId)
        {
            var user = context.Users
                .FirstOrDefault(u => u.Id == userId);

            return user == null
                ? false
                : user.IsAdmin;
        }
    }
}
