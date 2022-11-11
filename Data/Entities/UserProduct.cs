namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Mapping class for users and products.
    /// </summary>
    public class UserProduct
    {
        /// <summary>
        /// ID of user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// The user related to the product.
        /// </summary>
        [Required]
        public User User { get; set; } = null!;

        /// <summary>
        /// ID of product.
        /// </summary>
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }

        /// <summary>
        /// The product related to the user.
        /// </summary>
        [Required]
        public Product Product { get; set; } = null!;
    }
}
