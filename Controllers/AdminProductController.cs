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

            var model = new ProductFormViewModel()
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
        public async Task<IActionResult> Add(ProductFormViewModel model)
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

        /// <summary>
        /// Get request for editing a product.
        /// </summary>
        /// <param name="id">Id for the product that the admin wants to edit.</param>
        /// <returns>Post request with model to edit.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            // **********************************************************************************
            // TODO: Confirm if it's pointless to convert between database entity and view model.
            //       ----- If so, validation should be done differently.
            // **********************************************************************************
            var entity = await productService.GetAsync(id);
            var model = new ProductFormViewModel()
            {
                Name = entity.Name,
                Price = entity.Price,
                ImageUrl = entity.ImageUrl,
                Color = entity.Color,
                Description = entity.Description,
                Categories = await categoryService.GetOfficialAsync(),
                CategoryId = entity.CategoryId
            };

            return View(model);
        }

        /// <summary>
        /// Post request for editing a product.
        /// </summary>
        /// <param name="model">View model with validations.</param>
        /// <returns>If model is valid, user is redirected to admin category panel. Otherwise, they are prompted to edit the name again.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetOfficialAsync();
                return View(model);
            }

            await productService.EditAsync(model);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }

        /// <summary>
        /// Delete product from database.
        /// </summary>
        /// <param name="id">Id of product that will be deleted.</param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Delete(int id)
        {
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction("PLACE", "HOLDER");
            }

            await productService.DeleteAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }
    }
}
