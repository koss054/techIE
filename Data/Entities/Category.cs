namespace techIE.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Category entity. Lists the available categories.
    /// The official and marketplace product use the same entity for their category.
    /// </summary>
    public class Category
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
        public string Name { get; set; } = null!;

        /// <summary>
        /// The value is true if the category is used in the official store page.
        /// The value is false if the category is used only in the user marketplace.
        /// </summary>
        public bool IsOfficial { get; set; }
    }
}
