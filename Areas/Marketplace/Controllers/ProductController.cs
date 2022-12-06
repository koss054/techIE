namespace techIE.Areas.Marketplace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Infrastructure;

    using Models.Products;

    using techIE.Controllers;

    /// <summary>
    /// Product controller for user marketplace.
    /// Users can add products, view their details and make orders.
    /// </summary>
    [Area("Marketplace")]
    public class ProductController : BaseController
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(
            IUserService _userService,
            IProductService _productService,
            ICategoryService _categoryService)
        {
            userService = _userService;
            productService = _productService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// Adding a product to the user marketplace.
        /// Any logged in user can do this.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductFormViewModel
            {
                Categories = await categoryService.GetAllAsync()
            };

            return View(model);
        }

        /// <summary>
        /// Adds a product to the user marketplace if the model is valid.
        /// IsOfficial is always false, as the product is just for the marketplace.
        /// </summary>
        /// <param name="model">Product that we are adding to the marketplace.</param>
        /// <returns>Redirects to marketplace index page if successful. Otherwise, the user is promted to enter their product again.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllAsync();
                return View(model);
            }

            // As this product is added in the user marketplace it's not considered one of techIE's official products.
            model.IsOfficial = false;
            await productService.AddAsync(model, this.User.Id());
            return RedirectToAction(
                RedirectPaths.AddMarketplaceProductPage,
                RedirectPaths.AddMarketplaceProductController);
        }

        /// <summary>
        /// Product details for marketplace products.
        /// </summary>
        /// <param name="id">Id of the product the user wants to see.</param>
        /// <returns>Page with more details about the selected product.</returns>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var seller = await userService.GetUserByProductIdAsync(id);
            if (seller == null)
            {
                return BadRequest();
            }

            var model = await productService.GetDetailedAsync(id, seller);
            return View(model);
        }
    }
}
