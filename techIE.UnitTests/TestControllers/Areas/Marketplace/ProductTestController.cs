namespace techIE.Areas.Marketplace.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

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
    public class ProductTestController : BaseController
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;
        private readonly string testUserId = "fake-guid-id";

        public ProductTestController(
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
                Categories = await categoryService.GetAllAvailableAsync()
            };

            if (model.Categories.Count() == 0)
            {
                return RedirectToAction(
                    RedirectPaths.NoMarketplaceCategoriesPage,
                    RedirectPaths.NoMarketplaceCategoriesController);
            }

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
                model.Categories = await categoryService.GetAllAvailableAsync();
                return View(model);
            }

            // As this product is added in the user marketplace it's not considered one of techIE's official products.
            model.IsOfficial = false;
            await productService.AddAsync(model, testUserId);
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
            if (model == null || model.IsDeleted)
            {
                return NotFound();
            }

            // Redirects the user to the Official area, if the product is official.
            if (model.IsOfficial)
            {
                return RedirectToAction(
                    RedirectPaths.ProductIsOfficialPage,
                    RedirectPaths.ProductIsOfficialController,
                    new { id = model.Id, area = RedirectPaths.ProductIsOfficialArea });
            }

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
            if (await productService.IsUserSellerAsync(id, testUserId) == false)
            {
                return NotFound();
            }

            var model = await productService.GetFormModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Categories = await categoryService.GetAllAvailableAsync();
            if (model.Categories.Count() == 0)
            {
                return RedirectToAction(
                    RedirectPaths.NoMarketplaceCategoriesPage,
                    RedirectPaths.NoMarketplaceCategoriesController);
            }

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
                model.Categories = await categoryService.GetAllAvailableAsync();
                return View(model);
            }

            await productService.EditAsync(model);
            return RedirectToAction(
                RedirectPaths.MarketplaceEditPage,
                RedirectPaths.MarketplaceEditController);
        }

        /// <summary>
        /// Delete product from the marketplace.
        /// </summary>
        /// <param name="id">Id of product that will be deleted.</param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            var isSeller = await productService.IsUserSellerAsync(id, testUserId);
            if (isSeller == false)
            {
                return NotFound();
            }

            await productService.DeleteAsync(id);
            return RedirectToAction(
                RedirectPaths.DeleteMarketplaceProductPage,
                RedirectPaths.DeleteMarketplaceProductController,
                new { area = "" });
        }

        /// <summary>
        /// Restore a product to the marketplace.
        /// </summary>
        /// <param name="id">Id of product that will be restored.</param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Restore(int id)
        {
            var isSeller = await productService.IsUserSellerAsync(id, testUserId);
            if (isSeller == false)
            {
                return NotFound();
            }

            await productService.RestoreAsync(id);
            return RedirectToAction(
                RedirectPaths.DeleteMarketplaceProductPage,
                RedirectPaths.DeleteMarketplaceProductController,
                new { area = "" });
        }
    }
}
