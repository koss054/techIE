namespace techIE.Areas.Official.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using techIE.Controllers;
    using techIE.Contracts;

    /// <summary>
    /// Controller that handles the logic for techIE's official store.
    /// Only items with IsOfficial == true will be displayed here.
    /// </summary>
    [Area("Official")]
    public class StoreController : BaseController
    {
        private readonly IProductService productService;

        public StoreController(IProductService _productService)
        {
            productService = _productService;
        }

        /// <summary>
        /// The index page of techIE's official store.
        /// Always displays three random products.
        /// The users can select which category they'd like to see from here as well.
        /// </summary>
        /// <returns>Index page of the official StoreController.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Value in async method is true so only official products are taken.
            var model = await productService.GetThreeRandomAsync(true);
            return View(model);
        }

        /// <summary>
        /// Page for all of techIE's official phones.
        /// Used in the official StoreController.
        /// </summary>
        /// <returns>Store page for all official phones.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Phones()
        {
            // Value in async method is true so only official products are taken.
            var model = await productService.GetAllAsync(true);
            return View(model);
        }

        /// <summary>
        /// Page for all of techIE's official laptops.
        /// Used in the official StoreController.
        /// </summary>
        /// <returns>Store page for all official laptops.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Laptops()
        {
            // Value in async method is true so only official products are taken.
            var model = await productService.GetAllAsync(true);
            return View(model);
        }

        /// <summary>
        /// Page for all of techIE's official smart watches.
        /// Used in the official StoreController.
        /// </summary>
        /// <returns>Store page for all official smart watches.</returns>
        [AllowAnonymous]
        public async Task<IActionResult> Smart()
        {
            // Value in async method is true so only official products are taken.
            var model = await productService.GetAllAsync(true);
            return View(model);
        }
    }
}
