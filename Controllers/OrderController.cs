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
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }

        /// <summary>
        /// Checkout the products in the current cart of the user.
        /// </summary>
        /// <param name="cartId">The cart that is being finalized.</param>
        /// <returns>Redirects the user to their order history page.</returns>
        public async Task<IActionResult> Finish(int cartId)
        {
            await orderService.FinishAsync(cartId);
            return RedirectToAction("Inspect", "Cart");
        }

        public async Task<IActionResult> History()
        {
            var model = await orderService.GetHistoryAsync(this.User.Id());
            return View(model);
        }
    }
}
