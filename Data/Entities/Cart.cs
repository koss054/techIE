namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Tracks the products the user wants to buy.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Id of the cart.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// If the user is still filling up the same cart.
        /// If not, a new cart is created.
        /// </summary>
        [Required]
        public bool IsCurrent { get; set; }

        /// <summary>
        /// ID of the user who is filling up the cart.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// User who is filling up the cart.
        /// </summary>
        [Required]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Order))]
        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        /// <summary>
        /// Mapping table for the Cart and the Product.
        /// </summary>
        public virtual ICollection<CartProduct> CartsProducts { get; set; } = new List<CartProduct>();
    }
}
