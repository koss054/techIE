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

        /// <summary>
        /// Edit a product which the user has created.
        /// If the user isn't the one who has created the product, 404 is returned.
        /// </summary>
        /// <param name="id">Id of the product that is being edited.</param>
        /// <returns>NotFound if user isn't seller or if product with provided Id doesn't exist. Otherwise proceeds to post request.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (await productService.IsUserSellerAsync(id, this.User.Id()) == false)
            {
                return NotFound();
            }

            var model = await productService.GetFormModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Categories = await categoryService.GetAllAsync();
            return View(model);
        }

        /// <summary>
        /// Edits the marketplace product.
        /// </summary>
        /// <param name="model">Product that is being edited passed as a model to visualize on page.</param>
        /// <returns>The same page if model validations don't pass. Otherwise redirects to the marketplace store index page.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetAllAsync();
                return View(model);
            }

            await productService.EditAsync(model);
            return RedirectToAction(
                RedirectPaths.MarketplaceEditPage,
                RedirectPaths.MarketplaceEditController);
        }
    }
}
