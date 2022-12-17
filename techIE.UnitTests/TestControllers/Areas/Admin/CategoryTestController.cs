namespace techIE.UnitTests.TestControllers.Areas.Admin
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Contracts;

    using techIE.Controllers;
    using techIE.Models.Categories;
    using techIE.Constants;

    /// <summary>
    /// Controller managing the actions which are available in the admin category panel.
    /// No views pages are used here.
    /// </summary>
    [Area("Admin")]
    public class CategoryTestController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryTestController(ICategoryService _categoryService)
        {
            categoryService = _categoryService;
        }

        /// <summary>
        /// Get request for adding a category.
        /// </summary>
        /// <returns>Passed the necessary form model for the post request.</returns>
        [HttpGet]

        public IActionResult Add(bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            var model = new CategoryFormViewModel();
            return View(model);
        }

        /// <summary>
        /// Post request for adding a category/updating existing categories.
        /// </summary>
        /// <param name="model">View model containing all of the categories and the add view model.</param>
        /// <returns>The same view page on successfull category update.</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CategoryFormViewModel model, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

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
        /// Get request for editing a category.
        /// </summary>
        /// <param name="id">Id of the category that the user wants to edit.</param>
        /// <returns>CategoryFormViewModel to post request, allowing the user to edit the name of the selected category.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            var entity = await categoryService.GetAsync(id);
            if (entity == null)
            {
                return NotFound();
            }

            var model = new CategoryFormViewModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                IsOfficial = entity.IsOfficial
            };

            return View(model);
        }

        /// <summary>
        /// Post request for editing a category.
        /// </summary>
        /// <param name="model">View model used to ensure that the new name covers the needed validations.</param>
        /// <returns>If model is valid, user is redirected to admin category panel. Otherwise, they are prompted to edit the name again.</returns>
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormViewModel model, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await categoryService.EditAsync(model);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }

        /// <summary>
        /// Toggle the IsOfficial category property.
        /// </summary>
        /// <param name="id">Id of the category that should be verified.</param>
        /// <returns></returns>
        public async Task<IActionResult> Verify(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await categoryService.VerifyAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }

        /// <summary>
        /// Delete category from list.
        /// </summary>
        /// <param name="id">Id of category that will be deleted.</param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Delete(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            await categoryService.DeleteAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }

        /// <summary>
        /// Restore category to list.
        /// </summary>
        /// <param name="id">Id of category that will be restored.</param>
        /// <returns>Returns to panel page if successful.</returns>
        public async Task<IActionResult> Restore(int id, bool isUserAdmin)
        {
            if (!isUserAdmin)
            {
                return Unauthorized();
            }

            await categoryService.RestoreAsync(id);
            return RedirectToAction(
                RedirectPaths.UpdateCategoryPage,
                RedirectPaths.UpdateCategoryController);
        }
    }
}
