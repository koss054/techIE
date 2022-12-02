namespace techIE.Areas.Marketplace.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using techIE.Contracts;
    using techIE.Controllers;
    using techIE.Models.Products;

    /// <summary>
    /// Store controller for the user marketplace.
    /// Allows the users to browse the listings of others and create their own.
    /// </summary>
    [Area("Marketplace")]
    public class StoreController : BaseController
    {
        private readonly IProductService productService;
        private readonly ICategoryService categoryService;

        public StoreController(
            IProductService _productService,
            ICategoryService _categoryService)
        {
            productService = _productService;
            categoryService = _categoryService;
        }

        /// <summary>
        /// The index page of the user marketplace.
        /// Gets three random products.
        /// False is passed to the productService method so only products added by the users are taken.
        /// Other user marketplace options can also be accessed from here.
        /// </summary>
        /// <returns>The index page of the marketplace StoreController.</returns>
        public async Task<IActionResult> Index()
        {
            // Value in async method is false so only user added products are added.
            var model = await productService.GetThreeRandomAsync(false);
            return View(model);
        }

        /// <summary>
        /// The explore page of the user marketplace.
        /// Users can see all of the products listed by each user.
        /// They can sort the categories from the search bar.
        /// </summary>
        /// <returns>The explore page of the marketplace StoreController.</returns>
        public async Task<IActionResult> Explore([FromQuery] ProductQueryViewModel query)
        {
            var productCategories = await categoryService.GetAllNamesAsync();
            var queryResult = await productService.GetSearchResultAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                ProductQueryViewModel.ProductsPerPage);

            query.TotalProductsCount = queryResult.TotalProductCount;
            query.Products = queryResult.Products;
            query.Categories = productCategories;
            return View(query);
        }
    }
}
