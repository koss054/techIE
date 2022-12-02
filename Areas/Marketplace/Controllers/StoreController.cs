namespace techIE.Areas.Marketplace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using techIE.Contracts;
    using techIE.Controllers;

    /// <summary>
    /// Store controller for the user marketplace.
    /// Allows the users to browse the listings of others and create their own.
    /// </summary>
    [Area("Marketplace")]
    public class StoreController : BaseController
    {
        private readonly IProductService productService;

        public StoreController(IProductService _productService)
        {
            productService = _productService;
        }

        /// <summary>
        /// The index page of the user marketplace.
        /// Gets three random products.
        /// False is passed to the productService method so only products added by the users are taken.
        /// Other user marketplace options can also be accessed from here.
        /// </summary>
        /// <returns>The index page of the marketplace StoreController.</returns>
        public async Task<IActionResult> Index()
        {
            // Value in async method is false so only user added products are added.
            var model = await productService.GetThreeRandomAsync(false);
            return View(model);
        }

        /// <summary>
        /// The explore page of the user marketplace.
        /// Users can see all of the products listed by each user.
        /// They can sort the categories from the search bar.
        /// </summary>
        /// <returns>The explore page of the marketplace StoreController.</returns>
        public async Task<IActionResult> Explore()
        {
            return View();
        }
    }
}
