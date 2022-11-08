namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Contracts;
    using Data.Entities.Enums;

    using static Constants.DataConstants.Product;

    /// <summary>
    /// Official Product entity.
    /// The entity is used to manage products for the official techIE store.
    /// </summary>
    public class OfficialProduct : IProduct
    {
        /// <summary>
        /// Product ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// Visualized on all pages with product.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Price of the product.
        /// Visualized on all pages with product.
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Image of the product.
        /// Visualized on all pages with product.
        /// techIE owns imgur and wants the users the use their image hosting website to keep the product images ;)
        /// </summary>
        [Required]
        public string Image { get; set; } = null!;

        /// <summary>
        /// Color of the product.
        /// Changes the color of the product image.
        [Required]
        public Color Color { get; set; } = Color.Black;


        /// <summary>
        /// Description of the product.
        /// Visualized only on product page.
        /// </summary>
        [Required]
        [StringLength(MaxDescriptionLength)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// True if listed by techIE.
        /// False if listed by user.
        /// </summary>
        [Required]
        public bool IsOfficial { get; set; }

        /// <summary>
        /// Foreign key for the category.
        /// </summary>
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        /// <summary>
        /// Category of the product.
        /// Depending on it the product is visualized or not (in the store/marketplace).
        /// </summary>
        [Required]
        public Category Category { get; set; } = null!;

        /// <summary>
        /// Foreign key for the user.
        /// </summary>
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        /// <summary>
        /// User who has created the official product.
        /// Only users who are admins can create products for the techIE store.
        /// </summary>
        [Required]
        public User User { get; set; } = null!;
    }
}
