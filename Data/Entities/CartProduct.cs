namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Mapping table for Cart and Product.
    /// </summary>
    public class CartProduct
    {
        /// <summary>
        /// ID of cart.
        /// </summary>
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        /// <summary>
        /// The cart related to the product.
        /// </summary>
        [Required]
        public Cart Cart { get; set; } = null!;

        /// <summary>
        /// The times a user wants to buy a product that's in the cart.
        /// </summary>
        [Required]
        public int ProductQuantity { get; set; }

        /// <summary>
        /// ID of product.
        /// </summary>
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        /// <summary>
        /// The product related to the cart.
        /// </summary>
        [Required]
        public Product Product { get; set; } = null!;
    }
}
