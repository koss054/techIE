namespace techIE.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Models.Categories;

    /// <summary>
    /// Controller for the admin access.
    /// Only accessible by IsAdmin == true; users.
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;

        public AdminController(
            IUserService _userService, 
            ICategoryService _categoryService)
        {
            userService = _userService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user is admin, they can manage all categories.
        /// </summary>
        /// <returns>CategoryModel to view for HttpPost.</returns>
        [HttpGet]
        public IActionResult Categories()
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
        /// Post request for adding a category/updating existing categories.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The same view page on successfull category update.</returns>
        [HttpPost]
        public async Task<IActionResult> Categories(AddCategoryViewModel model)
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
