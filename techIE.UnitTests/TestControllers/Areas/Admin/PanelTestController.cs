namespace techIE.UnitTests.TestControllers.Areas.Admin
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Contracts;

    using techIE.Controllers;

    /// <summary>
    /// Controller for the admin access.
    /// Only accessible by users with the Administrator/Admin role.
    /// </summary>
    [Area("Admin")]
    public class PanelTestController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IProductService productService;

        public PanelTestController(
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
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of ClaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Panel index page.</returns>
        [HttpGet]
        public IActionResult Index(bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            return View();
        }

        /// <summary>
        /// Checks if the current user is an admin. If not, they are redirected.
        /// If user is admin, they can manage all categories.
        /// </summary>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of ClaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Category panel page.</returns>
        [HttpGet]
        public async Task<IActionResult> Categories(bool isUserAdmin)
        {
            if (!isUserAdmin)
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
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of ClaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Product panel page.</returns>
        [HttpGet]
        public async Task<IActionResult> Products(bool isUserAdmin)
        {
            if (!isUserAdmin)
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
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of ClaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Page, informing the admin that no official categories are available.</returns>
        [HttpGet]
        public async Task<IActionResult> Empty(bool isUserAdmin)
        {
            if (!isUserAdmin)
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
