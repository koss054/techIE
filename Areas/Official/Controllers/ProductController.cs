namespace techIE.Areas.Official.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;
    using Contracts;

    /// <summary>
    /// Official store controller for products.
    /// Allows the user to check product details, order the product, and for admins - update the product.
    /// </summary>
    [Area("Official")]
    public class ProductController : BaseController
    {
        private readonly IUserService userService;
        private readonly IProductService productService;

        public ProductController(
            IUserService _userService,
            IProductService _productService)
        {
            userService = _userService;
            productService = _productService;
        }

        /// <summary>
        /// Details page for official techIE products.
        /// </summary>
        /// <param name="id">Id of product.</param>
        /// <returns>View with additional details for selected product.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var seller = await userService.GetUserByProductIdAsync(id);
            if (seller == null)
            {
                return BadRequest();
            }

            var model = await productService.GetDetailedAsync(id, seller);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}
