namespace techIE.UnitTests.Services
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
        private ICartService cartService;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();

            cartService = new CartService(context);
        }

        [Test]
        public async Task Test_AddProductAsync_ReturnsSuccessful()
        {
            var productId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await cartService.AddProductAsync(productId, userId);

            Assert.AreEqual(CartAction.Successful, action);
        }

        [Test]
        public async Task Test_AddProductAsync_ReturnsDuplicate ()
        {
            var productId = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await cartService.AddProductAsync(productId, userId);

            Assert.AreEqual(CartAction.Duplicate, action);
        }

        [Test]
        public async Task Test_AddProductAsync_ReturnsFailed()
        {
            var productId = 4;
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var action = await cartService.AddProductAsync(productId, userId);

            Assert.AreEqual(CartAction.Failed, action);
        }

        [Test]
        public async Task Test_IsCartForUserAsync_ReturnsTrue()
        {
            var cartId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.True(await cartService.IsCartForUserAsync(cartId, userId));
        }

        [Test]
        public async Task Test_IsCartForUserAsync_ReturnsFalse()
        {
            var cartId = 3;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.False(await cartService.IsCartForUserAsync(cartId, userId));
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_ValidUser()
        {
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var cart = await cartService.GetCurrentCartAsync(userId);
            var dbCart = context.Carts.FirstOrDefault(c => c.UserId == userId && c.IsCurrent);

            Assert.AreEqual(dbCart.Id, cart.Id);
            Assert.AreEqual(dbCart.UserId, cart.UserId);
            Assert.AreEqual(dbCart.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_InvalidUser()
        {
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var cart = await cartService.GetCurrentCartAsync(userId);

            // Service method returns empty model if user doesn't have a current cart.
            var cartModel = new CartViewModel();

            Assert.AreEqual(cartModel.Id, cart.Id);
            Assert.AreEqual(cartModel.UserId, cart.UserId);
            Assert.AreEqual(cartModel.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_ValidId()
        {
            var cartId = 1;
            var cart = await cartService.GetCartAsync(cartId);
            var dbCart = context.Carts.FirstOrDefault(c => c.Id == cartId);

            Assert.AreNotEqual(null, cart);
            Assert.AreEqual(cart.UserId, cart.UserId);
            Assert.AreEqual(cart.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_InvalidId()
        {
            var cartId = 999;
            var cartModel = new CartViewModel();

            // Service method returns an empty view model if the provided Id doesn't match any cart in the db.
            var cart = await cartService.GetCartAsync(cartId);

            Assert.AreEqual(cartModel.Id, cart.Id);
            Assert.AreEqual(cartModel.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetTotalAsync_ValidId()
        {
            var firstCartId = 3;
            var firstExpectedTotal = 800;
            var firstReturnedTotal = await cartService.GetTotalAsync(firstCartId);

            var secondCartId = 2;
            var secondExpectedTotal = 1200;
            var secondReturnedTotal = await cartService.GetTotalAsync(secondCartId);

            Assert.AreEqual(firstExpectedTotal, firstReturnedTotal);
            Assert.AreEqual(secondExpectedTotal, secondReturnedTotal);
        }

        [Test]
        public async Task Test_GetTotalAsync_InvalidId()
        {
            var cartId = 999;

            // Service method returns 0 if cart isn't found.
            var returnedTotal = await cartService.GetTotalAsync(cartId);

            Assert.AreEqual(0, returnedTotal);
        }

        [Test]
        public async Task RemoveProductAsync_ReduceQuantity()
        {
            var cartId = 2;
            var productId = 3;

            // Initial product quantity is 4.
            var expectedQuantity = 3;
            var cartProducts = context.CartsProducts
                .First(cp => cp.CartId == cartId && cp.ProductId == productId);

            await cartService.RemoveProductAsync(cartId, productId);

            Assert.AreEqual(expectedQuantity, cartProducts.ProductQuantity);
        }

        [Test]
        public async Task RemoveProductAsync_RemoveProduct()
        {
            var cartId = 3;
            var productId = 3;

            await cartService.RemoveProductAsync(cartId, productId);

            var cartProducts = context.CartsProducts
                .FirstOrDefault(cp => cp.CartId == cartId && cp.ProductId == productId);


            Assert.AreEqual(null, cartProducts);
        }
        // Not testing invalid case, since nothing happens to the db with invalid parameters.

        [Test]
        public async Task RemoveAllProductsAsync_Valid()
        {
            var cartId = 3;
            await cartService.RemoveAllProductsAsync(cartId);
            var cartProducts = context.CartsProducts.FirstOrDefault(cp => cp.CartId == cartId);

            Assert.AreEqual(null, cartProducts);
        }
        // Not testing invalid, as nothing happens when an invalid cartId is passed.
    }
}