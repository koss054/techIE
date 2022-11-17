namespace techIE.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using Contracts;

    /// <summary>
    /// Controller for the admin access.
    /// Only accessible by IsAdmin == true; users.
    /// </summary>
    public class AdminController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public AdminController(
            IUserService _userService, 
            ICategoryService _categoryService,
            IProductService _productService)
        {
            userService = _userService;
            categoryService = _categoryService;
            productService = _productService;
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user is admin, they can manage all categories.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            var model = await categoryService.GetAllAsync();
            return View(model);
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user is admin, they can manage all official products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            var model = await productService.GetAllOfficialAsync();
            return View(model);
        }
    }
}
