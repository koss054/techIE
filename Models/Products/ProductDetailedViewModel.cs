namespace techIE.Models.Products
{
    using Data.Entities.Enums;

    /// <summary>
    /// Product detailed view model.
    /// Used when the user selects a product and opens its page.
    /// Has more product details - e.g. color & description.
    /// </summary>
    public class ProductDetailedViewModel
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

        /// <summary>
        /// Color of the product.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Description of the product.
        /// Visualized only on product page.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// User who is selling the product.
        /// </summary>
        public UserViewModel Seller { get; set; } = null!;
    }
}
