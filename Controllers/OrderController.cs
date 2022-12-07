namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for managing orders.
    /// Used in both official and marketplace area.
    /// </summary>
    public class OrderController : BaseController
    {
        /// <summary>
        /// Adding a product to the current order.
        /// </summary>
        /// <param name="id">Id of product that is being ordered.</param>
        public async Task<IActionResult> Add(int id)
        {
            return View();
        }
    }
}
