namespace techIE.Models.Carts
{
    using Models.Products;

    public class CartViewModel
    {
        /// <summary>
        /// Id of the cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of the user who is filling up the cart.
        /// </summary>
        public string UserId { get; set; } = null!;

        /// <summary>
        /// Total of all products that are currently in the cart.
        /// </summary>
        public decimal Total { get; set; }

        /// <summary>
        /// If the user is still filling up the same cart.
        /// If not, a new cart is created.
        /// Used in partial view checks when in this view model.
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        /// Products that are in the current cart.
        /// </summary>
        public IEnumerable<ProductCartViewModel> Products { get; set; } = new List<ProductCartViewModel>();
    }
}
