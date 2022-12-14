namespace techIE.UnitTests
{
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Services;
    using techIE.Models.Carts;
    using techIE.Data.Entities.Enums;

    using techIE.UnitTests.Data;

    [TestFixture]
    public class CartServiceTests
    {
        private AppDbContext context;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();
        }

        [Test]
        public async Task Test_AddProductAsync_Successful()
        {
            ICartService service = new CartService(context);

            var productId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Successful);
        }

        [Test]
        public async Task Test_AddProductAsync_Duplicate ()
        {
            ICartService service = new CartService(context);

            var productId = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Duplicate);
        }

        [Test]
        public async Task Test_AddProductAsync_Failed()
        {
            ICartService service = new CartService(context);

            var productId = 4;
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Failed);
        }

        [Test]
        public async Task Test_IsCartForUserAsync_True()
        {
            ICartService service = new CartService(context);

            var cartId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.True(await service.IsCartForUserAsync(cartId, userId));
        }

        [Test]
        public async Task Test_IsCartForUserAsync_False()
        {
            ICartService service = new CartService(context);

            var cartId = 3;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.False(await service.IsCartForUserAsync(cartId, userId));
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_Valid()
        {
            ICartService service = new CartService(context);

            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var cart = await service.GetCurrentCartAsync(userId);
            var dbCart = context.Carts.FirstOrDefault(c => c.UserId == userId && c.IsCurrent);

            Assert.True(cart.Id == dbCart.Id);
            Assert.True(cart.UserId == dbCart.UserId);
            Assert.True(cart.IsCurrent == dbCart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_Invalid()
        {
            ICartService service = new CartService(context);

            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var cart = await service.GetCurrentCartAsync(userId);

            // Service method returns empty model if user doesn't have a current cart.
            var cartModel = new CartViewModel();

            Assert.True(cart.Id == cartModel.Id);
            Assert.True(cart.UserId == cartModel.UserId);
            Assert.True(cart.IsCurrent == cartModel.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_Valid()
        {
            ICartService service = new CartService(context);

            var cartId = 1;
            var cart = await service.GetCartAsync(cartId);
            var dbCart = context.Carts.FirstOrDefault(c => c.Id == cartId);

            Assert.True(cart != null);
            Assert.True(cart.UserId == cart.UserId);
            Assert.True(cart.IsCurrent == cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_Invalid()
        {
            ICartService service = new CartService(context);

            var cartId = 4;
            var cartModel = new CartViewModel();

            // Service method returns an empty view model if the provided Id doesn't match any cart in the db.
            var cart = await service.GetCartAsync(cartId);

            Assert.True(cart.Id == cartModel.Id);
            Assert.True(cart.IsCurrent == cartModel.IsCurrent);
        }

        [Test]
        public async Task Test_GetTotalAsync_Valid()
        {
            ICartService service = new CartService(context);

            var firstCartId = 3;
            var firstExpectedTotal = 800;
            var firstReturnedTotal = await service.GetTotalAsync(firstCartId);

            var secondCartId = 2;
            var secondExpectedTotal = 1200;
            var secondReturnedTotal = await service.GetTotalAsync(secondCartId);

            Assert.AreEqual(firstExpectedTotal, firstReturnedTotal);
            Assert.AreEqual(secondExpectedTotal, secondReturnedTotal);
        }

        [Test]
        public async Task Test_GetTotalAsync_Invalid()
        {
            ICartService service = new CartService(context);

            var cartId = 999;

            // Service method returns 0 if cart isn't found.
            var returnedTotal = await service.GetTotalAsync(cartId);

            Assert.AreEqual(0, returnedTotal);
        }

        [Test]
        public async Task RemoveProductAsync_ReduceQuantity()
        {
            ICartService service = new CartService(context);

            var cartId = 2;
            var productId = 3;

            // Initial product quantity is 4.
            var expectedQuantity = 3;
            var cartProducts = context.CartsProducts
                .First(cp => cp.CartId == cartId && cp.ProductId == productId);

            await service.RemoveProductAsync(cartId, productId);

            Assert.AreEqual(cartProducts.ProductQuantity, expectedQuantity);
        }

        [Test]
        public async Task RemoveProductAsync_RemoveProduct()
        {
            ICartService service = new CartService(context);

            var cartId = 3;
            var productId = 3;

            await service.RemoveProductAsync(cartId, productId);

            var cartProducts = context.CartsProducts
                .FirstOrDefault(cp => cp.CartId == cartId && cp.ProductId == productId);


            Assert.AreEqual(cartProducts, null);
        }
        // Not testing invalid case, since nothing happens to the db with invalid parameters.

        [Test]
        public async Task RemoveAllProductsAsync_Valid()
        {
            ICartService service = new CartService(context);

            var cartId = 3;
            await service.RemoveAllProductsAsync(cartId);
            var cartProducts = context.CartsProducts.FirstOrDefault(cp => cp.CartId == cartId);

            Assert.AreEqual(cartProducts, null);
        }
        // Not testing invalid, as nothing happens when an invalid cartId is passed.
    }
}