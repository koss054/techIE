﻿namespace techIE.Data.Entities
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
        /// If the order is the current order of an user.
        /// If it is, any product is added to it.
        /// After the order is finalized, new products are added to a new order.
        /// </summary>
        [Required]
        public bool IsCurrent { get; set; }

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

        /// <summary>
        /// A collection of the ordered products.
        /// The total value is calculated by using this property.
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
