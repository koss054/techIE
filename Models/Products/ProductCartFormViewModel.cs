namespace techIE.Models.Products
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCartFormViewModel
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
        /// Quantity of the product that is in the cart.
        /// </summary>
        [Required]
        public int Quantity { get; set; }
    }
}
