namespace techIE.Models.Orders
{
    public class OrderHistoryViewModel
    {
        /// <summary>
        /// Id of the order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Total value of the order.
        /// </summary>
        public decimal TotalValue { get; set; }

        /// <summary>
        /// Id of the cart related to the order.
        /// </summary>
        public int CartId { get; set; }
    }
}
