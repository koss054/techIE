namespace techIE.Models.Categories
{
    /// <summary>
    /// View model for displaying category information.
    /// </summary>
    public class CategoryViewModel
    {
        /// <summary>
        /// Category ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name. It is visualized in two cases: 
        /// when a user browses the store or marketplace;
        /// when a user adds a product to the marketplace.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// The value is true if the category is used in the official store page.
        /// The value is false if the category is used only in the user marketplace.
        /// </summary>
        public bool IsOfficial { get; set; }
    }
}
