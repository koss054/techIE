namespace techIE.Contracts
{
    using Models.Orders;

    /// <summary>
    /// Handling all order logic.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// When user "finishes order" from cart page, the cart is added to the order, and is available in the history.
        /// </summary>
        /// <param name="cartId">Cart that is being bought.</param>
        Task FinishAsync(int cartId);

        /// <summary>
        /// Gets all orders for the current user.
        /// </summary>
        /// <param name="userId">Id of the user that is trying to view their orders.</param>
        /// <returns>List of the order of the requested user.</returns>
        Task<IEnumerable<OrderHistoryViewModel>> GetHistoryAsync(string userId);
    }
}
