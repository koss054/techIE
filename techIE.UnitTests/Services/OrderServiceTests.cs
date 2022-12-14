namespace techIE.UnitTests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Services;

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
            var cartId = 1;
            var cart = await context.Carts.FirstOrDefaultAsync(c => c.Id == cartId);

            await orderService.FinishAsync(cartId);
            var order = await context.Orders.FirstOrDefaultAsync();

            Assert.AreNotEqual(order, null);
            Assert.AreEqual(cart.IsCurrent, false);
            Assert.AreEqual(await cartService.GetTotalAsync(cartId), order.TotalValue);
        }

        [Test]
        public async Task Test_FinishAsync_InvalidCartId()
        {
            var cartId = 999;

            await orderService.FinishAsync(cartId);
            var order = await context.Orders.FirstOrDefaultAsync();

            Assert.AreEqual(order, null);
        }

        [Test]
        public async Task Test_GetHistoryAsync_ValidUser()
        {
            var expectedOrderCount = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            var orders = await orderService.GetHistoryAsync(userId);

            Assert.AreEqual(orders.Count(), expectedOrderCount);
        }

        [Test]
        public async Task Test_GetHistoryAsync_InvalidUser()
        {
            var expectedCount = 0;
            var userId = "invalid-user-id-guid";

            var orders = await orderService.GetHistoryAsync(userId);

            Assert.AreEqual(orders.Count(), expectedCount);
        }
    }
}
