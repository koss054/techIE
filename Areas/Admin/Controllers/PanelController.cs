namespace techIE.Areas.Admin.Controllers
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Mvc;

    using Contracts;
    using Infrastructure;
    using techIE.Controllers;

    /// <summary>
    /// Controller for the admin access.
    /// Only accessible by IsAdmin == true; users.
    /// </summary>
    [Area("Admin")]
    public class PanelController : BaseController
    {
        private readonly IUserService userService;
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public PanelController(
            IUserService _userService,
            ICategoryService _categoryService,
            IProductService _productService)
        {
            userService = _userService;
            categoryService = _categoryService;
            productService = _productService;
        }

        /// <summary>
        /// Index page for the Panel controller.
        /// Admins can access all of the admin pages from here.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            return View();
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user is admin, they can manage all categories.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
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
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var model = await productService.GetAllOfficialAsync();
            return View(model);
        }
    }
}
