namespace techIE.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Models.Categories;

    /// <summary>
    /// Controller for the category entity.
    /// Only accessible by admin users.
    /// The allowed users can add more categories to the database.
    /// </summary>
    public class CategoryController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public CategoryController(
            IUserService _userService, 
            ICategoryService _categoryService)
        {
            userService = _userService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// Get request for adding a category to the database.
        /// Only admins can add categories.
        /// The IsOfficial is set to "true" because of this. IsOfficial is false when an admin adds a category for the marketplace.
        /// If a non admin user tries to access this, they get redirected to an error page, informing them they don't have access.
        /// </summary>
        /// <returns>AddCategoryViewModel to post request.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(userId))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            // Admin adds a category.
            // Category is always official.
            var model = new AddCategoryViewModel();

            return View(model);
        }

        /// <summary>
        /// Post request for adding a category to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The same view, so the admin can add more categories if they want to.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.AddAsync(model);
            return RedirectToAction(
                RedirectPaths.AddCategoryPage,
                RedirectPaths.AddCategoryController);
        }
    }
}
