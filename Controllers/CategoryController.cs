namespace techIE.Controllers
{
    using System.Security.Claims;

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
        /// Get request for adding a category.
        /// </summary>
        /// <returns>Passed the necessary form model for the post request.</returns>
        [HttpGet]

        public IActionResult Add()
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
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
        public async Task<IActionResult> Add(CategoryFormViewModel model)
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
        /// Get request for editing a category.
        /// </summary>
        /// <param name="id">Id of the category that the user wants to edit.</param>
        /// <returns>CategoryFormViewModel to post request, allowing the user to edit the name of the selected category.</returns>
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // It's no issue that userId may be null.
            // IsAdminAsync returns false if no users' ID matches the provided one.
            if (!userService.IsAdminAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value))
            {
                return RedirectToAction("PLACEHOLDER", "PLACEHOLDER");
            }

            var entity = await categoryService.GetAsync(id);
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
        public async Task<IActionResult> Edit(CategoryFormViewModel model)
        {
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
