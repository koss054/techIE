﻿namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Models.Categories;

    /// <summary>
    /// Controller managing the actions which are available in the admin category panel.
    /// No views pages are used here.
    /// </summary>
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        public IActionResult Add()
        {
            var model = new AddCategoryViewModel();
            return View(model);
        }

        /// <summary>
        /// Post request for adding a category/updating existing categories.
        /// </summary>
        /// <param name="model">View model containing all of the categories and the add view model.</param>
        /// <returns>The same view page on successfull category update.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.AddAsync(model);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }

        /// <summary>
        /// Toggle the IsOfficial category property.
        /// </summary>
        /// <param name="id">Id of the category that should be verified.</param>
        /// <returns></returns>
        public async Task<IActionResult> Verify(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PLACE", "HOLDER");
            }

            await categoryService.VerifyAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("PLACE", "HOLDER");
            }

            await categoryService.DeleteAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }
    }
}
