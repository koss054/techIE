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
        /// Adding a product to the current order.
        /// </summary>
        /// <param name="id">Id of product that is being ordered.</param>
        /// <param name="store">Name of area from where the action has been called.</param>
        /// <returns>Redirects the user to appropriate store index page.</returns>
        public async Task<IActionResult> Add(int id, string store)
        {
            await orderService.AddAsync(id, this.User.Id());
            return RedirectToAction(
                RedirectPaths.AddProductToOrderPage,
                RedirectPaths.AddProductToOrderController,
                new { area = store });
        }
    }
}
