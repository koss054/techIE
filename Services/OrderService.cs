namespace techIE.Services
{
    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data;
    using Data.Entities;

    public class OrderService : IOrderService
    {
        private readonly AppDbContext context;

        public OrderService(AppDbContext _context)
        {
            context = _context;
        }
    }
}
