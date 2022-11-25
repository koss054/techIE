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
            var model = await productService.GetThreeRandomOfficialAsync();
            return View(model);
        }
    }
}
