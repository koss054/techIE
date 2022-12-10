namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Infrastructure;

    /// <summary>
    /// Controller for managing orders.
    /// Used in both official and marketplace area.
    /// </summary>
    public class OrderController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;

        public OrderController(
            ICartService _cartService,
            IOrderService _orderService)
        {
            cartService = _cartService;
            orderService = _orderService;
        }

        /// <summary>
        /// Checkout the products in the current cart of the user.
        /// </summary>
        /// <param name="cartId">The cart that is being finalized.</param>
        /// <returns>Redirects the user to their order history page.</returns>
        public async Task<IActionResult> Finish(int cartId)
        {
            if (await cartService.IsCartForUserAsync(cartId, this.User.Id()) == false)
            {
                return NotFound();
            }

            await orderService.FinishAsync(cartId);
            return RedirectToAction(
                RedirectPaths.FinishOrderPage,
                RedirectPaths.FinishOrderController);
        }

        public async Task<IActionResult> History()
        {
            var model = await orderService.GetHistoryAsync(this.User.Id());
            return View(model);
        }
    }
}
