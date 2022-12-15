namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Order entity.
    /// Tracks the orders of the user.
    /// Keeps the total value of the order, the user who has made the order and the products that were ordered.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Order ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Total value of the order.
        /// </summary>
        [Required]
        public decimal TotalValue { get; set; }

        /// <summary>
        /// ID of the user who has made the order.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// User who has made the order.
        /// </summary>
        [Required]
        public User User { get; set; } = null!;

        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        [Required]
        public Cart Cart { get; set; } = null!;
    }
}
