namespace techIE.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    using Constants;
    using Data.Entities.Enums;

    /// <summary>
    /// Query model used for the product search bar.
    /// </summary>
    public class ProductQueryViewModel
    {
        /// <summary>
        /// Number of products that are visualized per page.
        /// </summary>
        public const int ProductsPerPage = 3;

        /// <summary>
        /// The category of the queried products.
        /// </summary>
        public string Category { get; set; } = null!;

        /// <summary>
        /// Search term that shows products which contain it in their name or description.
        /// </summary>
        [Display(Name = DisplayNames.MarketplaceSearchTerm)]
        public string SearchTerm { get; set; } = null!;

        /// <summary>
        /// Enum responsible for the order of the visualized products.
        /// </summary>
        public ProductSorting Sorting { get; set; }

        /// <summary>
        /// Current page of products.
        /// </summary>
        public int CurrentPage { get; set; } = 1;

        /// <summary>
        /// The total amount of products which we are visualizing.
        /// </summary>
        public int TotalProductsCount { get; set; }

        /// <summary>
        /// Available categories for sorting.
        /// </summary>
        public IEnumerable<string> Categories { get; set; } = null!;

        /// <summary>
        /// Products which will be visualized depending on the properties above.
        /// </summary>
        public IEnumerable<ProductOverviewViewModel> Products { get; set; } = new List<ProductOverviewViewModel>();
    }
}
