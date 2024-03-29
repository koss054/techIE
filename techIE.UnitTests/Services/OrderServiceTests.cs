﻿#nullable disable
namespace techIE.UnitTests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;
    using Microsoft.EntityFrameworkCore;

    using techIE.Data;
    using techIE.Services;
    using techIE.Contracts;

    using techIE.UnitTests.Data;

    [TestFixture]
    public class OrderServiceTests
    {
        private AppDbContext context;
        private ICartService cartService;
        private IOrderService orderService;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();

            cartService = new CartService(context);
            orderService = new OrderService(context, cartService);
        }

        [Test]
        public async Task Test_FinishAsync_ValidCartId()
        {
            // Arrange
            var cartId = 4;
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);
            var expectedTotalValue = await cartService.GetTotalAsync(cartId);

            // Act
            await orderService.FinishAsync(cartId);
            // Getting the order that was added last.
            var order = await context.Orders.LastOrDefaultAsync();

            // Assert
            Assert.IsNotNull(order);
            Assert.IsNotNull(cart);
            Assert.AreEqual(false, cart.IsCurrent);
            Assert.AreEqual(expectedTotalValue, order.TotalValue);
        }

        [Test]
        public async Task Test_FinishAsync_InvalidCartId()
        {
            // Arrange
            var cartId = 999;
            var expectedOrderCount = 0;

            // Act
            // If cart is invalid, no order is added to the database.
            await orderService.FinishAsync(cartId);
            // Expected result is that no order with the provided cartId should be in the database.
            var order = await context.Orders.Where(o => o.CartId == cartId).ToListAsync();

            // Assert
            Assert.AreEqual(expectedOrderCount, order.Count);
        }

        [Test]
        public async Task Test_GetHistoryAsync_ValidUser()
        {
            // Arrange
            var expectedOrderCount = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            // Act
            var orders = await orderService.GetHistoryAsync(userId);

            // Assert
            Assert.AreEqual(expectedOrderCount, orders.Count());
        }

        [Test]
        public async Task Test_GetHistoryAsync_InvalidUser()
        {
            // Arrange
            var expectedCount = 0;
            var userId = "invalid-user-id-guid";

            // Act
            var orders = await orderService.GetHistoryAsync(userId);

            // Assert
            Assert.AreEqual(expectedCount, orders.Count());
        }
    }
}
