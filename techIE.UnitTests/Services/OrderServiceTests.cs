namespace techIE.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Data.Entities;
    using techIE.Services;

    public class MockedDb
    {

    }

    [TestFixture]
    public class OrderServiceTests
    {
        private AppDbContext contextMock;
        private CartService cartService;
        private OrderService orderService;

        //[SetUp]
        //public void SetUpDbContext()
        //{
        //    contextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        //}

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            contextMock = new AppDbContext(options);
            cartService = new CartService(contextMock);
            orderService = new OrderService(contextMock, cartService);
        }

        [Test]
        public async Task Test_FinishAsync_Valid()
        {
            var cart = new Cart()
            {
                Id = 1,
                IsCurrent = true,
            };

            contextMock.Carts.Add(cart);
            await orderService.FinishAsync(cart.Id);

            Assert.AreEqual(cart.IsCurrent, false);
        }
    }
}
