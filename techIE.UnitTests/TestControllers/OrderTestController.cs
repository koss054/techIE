namespace techIE.UnitTests.TestControllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;
    using techIE.Constants;
    using techIE.Contracts;

    /// <summary>
    /// Controller for managing orders.
    /// Used in both official and marketplace area.
    /// </summary>
    public class OrderTestController : BaseController
    {
        private readonly ICartService cartService;
        private readonly IOrderService orderService;
        private string testUserId = "fake-guid"; // Specifying this here, because I can't test the static ClaimsPrincipalExtensions User.Id().

        public OrderTestController(
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
            if (await cartService.IsCartForUserAsync(cartId, testUserId) == false)
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
            var model = await orderService.GetHistoryAsync(testUserId);
            return View(model);
        }
    }
}
