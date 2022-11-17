namespace techIE.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Models.Products;

    /// <summary>
    /// Controller for managing the official products from the admin product panel.
    /// </summary>
    public class AdminProductController : Controller
    {
        private readonly IUserService userService;
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public AdminProductController(
            IUserService _userService,
            IProductService _productService,
            ICategoryService _categoryService)
        {
            userService = _userService;
            productService = _productService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// Get request for adding a product from the admin product panel.
        /// Can't be accessed by users that aren't admins.
        /// </summary>
        /// <returns>Model to add to post request.</returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            var model = new AddProductViewModel()
            {
                Categories = await categoryService.GetOfficialAsync()
            };

            return View(model);
        }

        /// <summary>
        /// Post request for adding a product to the database.
        /// Can't be accessed by users that aren't admins.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Admin product panel on successful add. Otherwise, the user can try to add the product again.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetOfficialAsync();
                return View(model);
            }

            await productService.AddAsync(model);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }
    }
}
