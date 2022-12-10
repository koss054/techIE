namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Entities;
    using Models.Orders;

    public class OrderService : IOrderService
    {
        private readonly AppDbContext context;
        private readonly ICartService cartService;

        public OrderService(
            AppDbContext _context,
            ICartService _cartService)
        {
            context = _context;
            cartService = _cartService;
        }

        /// <summary>
        /// When user "finishes order" from cart page, the cart is added to the order, and is available in the history.
        /// </summary>
        /// <param name="cartId">Cart that is being bought.</param>
        public async Task FinishAsync(int cartId)
        {
            var cart = await context.Carts
                .FirstOrDefaultAsync(c => c.Id == cartId &&
                                          c.IsCurrent == true);

            if (cart != null)
            {
                cart.IsCurrent = false;
                var order = new Order()
                {
                    TotalValue = await cartService.GetTotalAsync(cartId),
                    UserId = cart.UserId,
                    User = cart.User,
                    CartId = cartId,
                    Cart = cart
                };

                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all orders for the current user.
        /// </summary>
        /// <param name="userId">Id of the user that is trying to view their orders.</param>
        /// <returns>List of the order of the requested user.</returns>
        public async Task<IEnumerable<OrderHistoryViewModel>> GetHistoryAsync(string userId)
        {
            var orders = await context.Orders
                .Where(o => o.UserId == userId)
                .Select(o => new OrderHistoryViewModel()
                {
                    Id = o.Id,
                    TotalValue = o.TotalValue,
                    CartId = o.CartId
                }).ToListAsync();

            return orders;
        }
    }
}
