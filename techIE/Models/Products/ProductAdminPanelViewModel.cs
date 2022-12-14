namespace techIE.Models.Products
{
    /// <summary>
    /// View model for displaying products in the admin product panel.
    /// </summary>
    public class ProductAdminPanelViewModel
    {
        /// <summary>
        /// Id of the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the product.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Category of the product.
        /// </summary>
        public string Category { get; set; } = null!;

        /// <summary>
        /// If true, it's official.
        /// </summary>
        public bool IsOfficial { get; set; }

        /// <summary>
        /// If true, product is deleted and is not available in the stores.
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
