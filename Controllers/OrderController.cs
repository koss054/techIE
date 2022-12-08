namespace techIE.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using Constants;
    using Contracts;
    using Infrastructure;

    /// <summary>
    /// Controller for managing orders.
    /// Used in both official and marketplace area.
    /// </summary>
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService _orderService)
        {
            orderService = _orderService;
        }
    }
}
