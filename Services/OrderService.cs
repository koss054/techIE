namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Entities;

    public class OrderService : IOrderService
    {
        private readonly AppDbContext context;

        public OrderService(AppDbContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Add a product to the current user order.
        /// </summary>
        /// <param name="productId">Id of product that is being added to the order.</param>
        /// <param name="userId">Id of the user that is adding a product to their order.</param>
        public async Task AddAsync(int productId, string userId)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.Id == productId);
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (product != null && user != null)
            {
                await AddToCurrentAsync(product, user);
            }
        }

        /// <summary>
        /// Add product to current user order.
        /// </summary>
        /// <param name="product">Product that is being added to the order.</param>
        /// <param name="user">User who is adding the product to their current order.</param>
        private async Task AddToCurrentAsync(Product product, User user)
        {
            var order = await context.Orders.FirstOrDefaultAsync(o => o.UserId == user.Id);
            if (order == null || order.IsCurrent == false)
            {
                order = new Order()
                {
                    TotalValue = 0,
                    IsCurrent = true,
                    User = user,
                };

                await context.Orders.AddAsync(order);
            }

            order.Products.Add(product);
            await context.SaveChangesAsync();
        }
    }
}
