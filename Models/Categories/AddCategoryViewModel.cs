namespace techIE.Models.Categories
{
    using System.ComponentModel.DataAnnotations;

    using static Constants.DataConstants.Category;

    /// <summary>
    /// View model used to add a category to the database.
    /// </summary>
    public class AddCategoryViewModel
    {
        /// <summary>
        /// Category ID.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Category name. It is visualized in two cases: 
        /// when a user browses the store or marketplace;
        /// when a user adds a product to the marketplace.
        /// </summary>
        [Required]
        [StringLength(MaxNameLength, MinimumLength = MinNameLength)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// The value is true if the category is used in the official store page.
        /// The value is false if the category is used only in the user marketplace.
        /// </summary>
        [Required]
        public bool IsOfficial { get; set; }
    }
}
