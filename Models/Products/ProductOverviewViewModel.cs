namespace techIE.Models.Products
{
    /// <summary>
    /// Product overview view model.
    /// Used for listing the product in the category page.
    /// Not showing additional product details - e.g. its color & description.
    /// </summary>
    public class ProductOverviewViewModel
    {
        /// <summary>
        /// Id of product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Price of product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Image url of product.
        /// techIE "owns" imgur and they use it to host the images.
        /// </summary>
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Category of the product.
        /// Depending on it the product is visualized or not (in the store/marketplace).
        /// </summary>
        public string Category { get; set; } = null!;
    }
}
