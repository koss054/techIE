namespace techIE.Services.Product
{
    using Models.Products;

    /// <summary>
    /// Model used for displaying products in the user marketplace.
    /// The total count is kept so the appropriate actions are available when a user wants to change the page.
    /// </summary>
    public class ProductQueryServiceModel
    {
        /// <summary>
        /// The total count of products in the user marketplace.
        /// Determines if the buttons for the previous/next page are available.
        /// </summary>
        public int TotalProductCount { get; set; }

        /// <summary>
        /// List containing the products for the user marketplace explore page.
        /// </summary>
        public IEnumerable<ProductOverviewViewModel> Products { get; set;} = new List<ProductOverviewViewModel>();
    }
}
