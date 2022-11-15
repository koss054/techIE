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
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var userId = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(userId))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            AdminCategoryViewModel model
                = await categoryService.GetAllAsync();

            return View(model);
        }
    }
}
