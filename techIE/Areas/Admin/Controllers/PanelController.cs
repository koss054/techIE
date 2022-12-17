namespace techIE.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Contracts;
    using Infrastructure;
    using techIE.Controllers;

    /// <summary>
    /// Controller for the admin access.
    /// Only accessible by users with the Administrator/Admin role.
    /// </summary>
    [Area("Admin")]
    public class PanelController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public PanelController(
            ICategoryService _categoryService,
            IProductService _productService)
        {
            categoryService = _categoryService;
            productService = _productService;
        }

        /// <summary>
        /// Index page for the Panel controller.
        /// Admins can access all of the admin pages from here.
        /// </summary>
        /// <returns>Panel index page.</returns>
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
        /// <returns>Category panel page.</returns>
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
        /// <returns>Product panel page.</returns>
        [HttpGet]
        public async Task<IActionResult> Products()
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var model = await productService.GetAllAdminAsync();
            return View(model);
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user tries to add/edit an official product and no category has IsDeleted == false, they get redirected here.
        /// </summary>
        /// <returns>Page, informing the admin that no official categories are available.</returns>
        [HttpGet]
        public async Task<IActionResult> Empty()
        {
            if (!this.User.IsAdmin())
            {
                return Unauthorized();
            }

            var officialCategories = await categoryService.GetOfficialAsync();
            if (officialCategories.Any())
            {
                return BadRequest();
            }

            return View();
        }
    }
}
