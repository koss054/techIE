﻿namespace techIE.UnitTests.TestControllers.Areas.Admin
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;

    using Models.Products;

    using techIE.Controllers;

    /// <summary>
    /// Controller for managing the official products from the admin product panel.
    /// </summary>
    [Area("Admin")]
    public class ProductTestController : BaseController
    {
        // Specifying this here, because I can't test the static ClaimsPrincipalExtensions User.Id().
        private readonly string testUserId = "fake-guid-id";
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public ProductTestController(
            IProductService _productService,
            ICategoryService _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// Get request for adding a product from the admin product panel.
        /// Can't be accessed by users that aren't admins.
        /// </summary>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Model to add to post request.</returns>
        [HttpGet]
        public async Task<IActionResult> Add(bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            var model = new ProductFormViewModel()
            {
                Categories = await categoryService.GetOfficialAsync()
            };

            if (!model.Categories.Any())
            {
                return RedirectToAction(
                    RedirectPaths.NoAvailableCategoriesPage,
                    RedirectPaths.NoAvailableCategoriesController);
            }

            return View(model);
        }

        /// <summary>
        /// Post request for adding a product to the database.
        /// Can't be accessed by users that aren't admins.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Admin product panel on successful add. Otherwise, the user can try to add the product again.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(ProductFormViewModel model, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.GetOfficialAsync();
                return View(model);
            }

            // Since this controller is in the Admin area, the added products are always official.
            model.IsOfficial = true;
            await productService.AddAsync(model, testUserId);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }

        /// <summary>
        /// Get request for editing a product.
        /// </summary>
        /// <param name="id">Id for the product that the admin wants to edit.</param>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Post request with model to edit.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            var model = await productService.GetFormModelAsync(id);
            if (model == null)
            {
                return NotFound();
            }

            model.Categories = await categoryService.GetOfficialAsync();
            if (!model.Categories.Any())
            {
                return RedirectToAction(
                    RedirectPaths.NoAvailableCategoriesPage,
                    RedirectPaths.NoAvailableCategoriesController);
            }

            return View(model);
        }

        /// <summary>
        /// Post request for editing a product.
        /// </summary>
        /// <param name="model">View model with validations.</param>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>If model is valid, user is redirected to admin category panel. Otherwise, they are prompted to edit the name again.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormViewModel model, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

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
        /// Delete product from the list.
        /// </summary>
        /// <param name="id">Id of product that will be deleted.</param>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Delete(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            await productService.DeleteAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }

        /// <summary>
        /// Restore a product to the list.
        /// </summary>
        /// <param name="id">Id of product that will be restored.</param>
        /// <param name="isUserAdmin">
        /// Temporary param for test controller.
        /// Otherwise an error for no instance of CLaimsPrincpialExtensions.IsAdmin().
        /// </param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Restore(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            await productService.RestoreAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateProductPage,
                RedirectPaths.UpdateProductController);
        }
    }
}
