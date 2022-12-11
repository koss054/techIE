namespace techIE.Models.Products
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Models.Categories;
    using Data.Entities;
    using Data.Entities.Enums;
    using Constants;

    using static Constants.DataConstants.Product;

    /// <summary>
    /// View model used for adding a product to the database/stores.
    /// Only admin accounts can add products to the official store.
    /// </summary>
    public class ProductFormViewModel
    {
        /// <summary>
        /// Id of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// Visualized on all pages with product.
        /// </summary>
        [Required(
            ErrorMessage = ErrorMessages.ProductNameRequiredErrorMessage)]
        [StringLength(
            MaxNameLength, 
            MinimumLength = MinNameLength, 
            ErrorMessage = ErrorMessages.ProductNameRangeErrorMessage)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Price of the product.
        /// Visualized on all pages with product.
        /// </summary>
        [Required]
        [Range(typeof(decimal), MinPriceValue, MaxPriceValue, ConvertValueInInvariantCulture = false)]
        public decimal Price { get; set; }

        /// <summary>
        /// Image of the product.
        /// Visualized on all pages with product.
        /// techIE owns imgur and wants the users the use their image hosting website to keep the product images ;)
        /// </summary>
        [Required]
        public string ImageUrl { get; set; } = null!;

        /// <summary>
        /// Color of the product.
        /// </summary>
        [Required]
        public Color Color { get; set; }

        /// <summary>
        /// Description of the product.
        /// Visualized only on product page.
        /// </summary>
        [Required]
        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
        public string Description { get; set; } = null!;

        /// <summary>
        /// True if listed by techIE.
        /// False if listed by user.
        /// </summary>
        [Required]
        public bool IsOfficial { get; set; }

        /// <summary>
        /// Id of the product category.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Selection of categories for the product that is being added.
        /// </summary>
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}
