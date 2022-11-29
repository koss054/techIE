namespace techIE.Areas.Marketplace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Models.Products;
    using techIE.Controllers;

    /// <summary>
    /// Product controller for user marketplace.
    /// Users can add products, view their details and make orders.
    /// </summary>
    [Area("Marketplace")]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductController(
            IProductService _productService,
            ICategoryService _categoryService)
        {
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

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetOfficialAsync();
                return View(model);
            }

            // As this product is added in the user marketplace it's not considered one of techIE's official products.
            model.IsOfficial = false;
            await productService.AddAsync(model);
            return RedirectToAction(
                RedirectPaths.AddMarketplaceProductPage,
                RedirectPaths.AddMarketplaceProductController);
        }
    }
}
