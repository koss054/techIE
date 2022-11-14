namespace techIE.Models.Categories
{
    /// <summary>
    /// View model for displaying and modifying category information.
    /// </summary>
    public class AdminCategoryViewModel
    {
        /// <summary>
        /// List of categories in database.
        /// Displays the categories in the admin view page.
        /// </summary>
        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        /// <summary>
        /// Manages the added category.
        /// Option appears in view after a button is pressed.
        /// </summary>
        public AddCategoryViewModel AddedCategory { get; set; } = null!;
    }
}
