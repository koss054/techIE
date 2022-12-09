namespace techIE.Contracts
{
    using Data.Entities.Enums;
    using Models.Carts;

    /// <summary>
    /// Handling all cart logic.
    /// </summary>
    public interface ICartService
    {
        /// <summary>
        /// Adds product to the cart.
        /// </summary>
        /// <param name="productId">Id of the product that is being added.</param>
        /// <param name="userId">Id of the user who is adding the product to the cart.</param>
        /// <returns>Successful if added. Duplicate if product is already in cart. Failed if product/user doesn't exist.</returns>
        Task<CartAction> AddProductAsync(int productId, string userId);

        /// <summary>
        /// Checks if the current user has a cart that is active.
        /// </summary>
        /// <param name="userId">Id of user.</param>
        /// <returns>True if user has an active cart. False if user doesn't.</returns>
        Task<bool> IsUserCartCurrent(string userId);

        /// <summary>
        /// Get the current cart of the user.
        /// </summary>
        /// <param name="userId">User whose cart we are trying to get.</param>
        /// <returns>CartViewModel used in the Inspect page, if there is a current cart active.</returns>
        Task<CartViewModel?> GetCurrentCart(string userId);

        /// <summary>
        /// Get the total of the cart.
        /// </summary>
        /// <param name="cartId">Id of the cart which total we want to get.</param>
        /// <returns>Total of all products in the requested cart.</returns>
        Task<decimal> GetTotalAsync(int cartId);

        /// <summary>
        /// Remove a product from the cart.
        /// </summary>
        /// <param name="cartId">Id of the cart that has the product being removed.</param>
        /// <param name="productId">Id of the product that is being removed.</param>
        Task RemoveProductAsync(int cartId, int productId);

        /// <summary>
        /// Removes all product from current cart.
        /// </summary>
        /// <param name="cartId">Cart with products to remove.</param>
        Task RemoveAllProductsAsync(int cartId);
    }
}
