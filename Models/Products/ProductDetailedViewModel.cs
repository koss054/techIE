namespace techIE.Models.Products
{
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
    }
}
