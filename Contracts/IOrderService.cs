namespace techIE.Contracts
{
    /// <summary>
    /// Handling all order logic.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Add a product to the current user order.
        /// </summary>
        /// <param name="productId">Id of product that is being added to the order.</param>
        /// <param name="userId">Id of the user that is adding a product to their order.</param>
        Task AddAsync(int productId, string userId);
    }
}
