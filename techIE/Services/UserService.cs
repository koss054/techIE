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
                    Email = u.Email
                }).FirstOrDefaultAsync(u => u.Id == userId);
        }

        /// <summary>
        /// Get user by product Id.
        /// Method used to display who has listed the product.
        /// </summary>
        /// <param name="productId">Id of the product whose user we want to find.</param>
        /// <returns>User if successful. Otherwise, null.</returns>
        public async Task<UserViewModel?> GetUserByProductIdAsync(int productId)
        {
            var userProduct = await context.UsersProducts.FirstOrDefaultAsync(up => up.ProductId == productId);
            if (userProduct != null)
            {
                return await this.GetUserAsync(userProduct.UserId) ?? null;
            }
            return null;
        }
    }
}
