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
        /// Products that are in the current cart.
        /// </summary>
        public IEnumerable<ProductCartFormViewModel> Products { get; set; } = new List<ProductCartFormViewModel>();
    }
}
