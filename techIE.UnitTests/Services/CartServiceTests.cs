#nullable disable
namespace techIE.UnitTests.Services
{
    using System.Linq;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using techIE.Data;
    using techIE.Services;
    using techIE.Contracts;
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
            // Arrange
            var productId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            // Act
            var action = await cartService.AddProductAsync(productId, userId);

            // Assert
            Assert.AreEqual(CartAction.Successful, action);
        }

        [Test]
        public async Task Test_AddProductAsync_ReturnsDuplicate ()
        {
            // Arrange
            var productId = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            // Act
            var action = await cartService.AddProductAsync(productId, userId);

            // Assert
            Assert.AreEqual(CartAction.Duplicate, action);
        }

        [Test]
        public async Task Test_AddProductAsync_ReturnsFailed()
        {
            // Arrange
            var productId = 999;
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";

            // Act
            var action = await cartService.AddProductAsync(productId, userId);

            // Assert
            Assert.AreEqual(CartAction.Failed, action);
        }

        [Test]
        public async Task Test_IsCartForUserAsync_ReturnsTrue()
        {
            // Arrange
            var cartId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            // Act
            var result = await cartService.IsCartForUserAsync(cartId, userId);

            // Assert
            Assert.True(result);
        }

        [Test]
        public async Task Test_IsCartForUserAsync_ReturnsFalse()
        {
            // Arrange
            var cartId = 3;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            // Act
            var result = await cartService.IsCartForUserAsync(cartId, userId);

            // Assert
            Assert.False(result);
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_ValidUser()
        {
            // Arrange
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var dbCart = context.Carts.FirstOrDefault(c => c.UserId == userId && c.IsCurrent);

            // Act
            var cart = await cartService.GetCurrentCartAsync(userId);

            // Assert
            Assert.AreEqual(dbCart.Id, cart.Id);
            Assert.AreEqual(dbCart.UserId, cart.UserId);
            Assert.AreEqual(dbCart.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_InvalidUser()
        {
            // Arrange
            // Service method returns empty model if user doesn't have a current cart.
            var cartModel = new CartViewModel();
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";

            // Act
            var cart = await cartService.GetCurrentCartAsync(userId);

            // Assert
            Assert.AreEqual(cartModel.Id, cart.Id);
            Assert.AreEqual(cartModel.UserId, cart.UserId);
            Assert.AreEqual(cartModel.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_ValidId()
        {
            // Arrange
            var cartId = 1;
            var dbCart = context.Carts.FirstOrDefault(c => c.Id == cartId);

            // Act
            var cart = await cartService.GetCartAsync(cartId);

            // Assert
            Assert.IsNotNull(cart);
            Assert.AreEqual(cart.UserId, cart.UserId);
            Assert.AreEqual(cart.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_InvalidId()
        {
            // Arrange
            // Service method returns an empty view model if the provided Id doesn't match any cart in the db.
            var cartModel = new CartViewModel();
            var cartId = 999;

            // Act
            var cart = await cartService.GetCartAsync(cartId);

            // Assert
            Assert.AreEqual(cartModel.Id, cart.Id);
            Assert.AreEqual(cartModel.IsCurrent, cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetTotalAsync_ValidId()
        {
            // Arrange
            var firstCartId = 3;
            var firstExpectedTotal = 800;
            var secondCartId = 2;
            var secondExpectedTotal = 1200;

            // Act
            var firstReturnedTotal = await cartService.GetTotalAsync(firstCartId);
            var secondReturnedTotal = await cartService.GetTotalAsync(secondCartId);

            // Assert
            Assert.AreEqual(firstExpectedTotal, firstReturnedTotal);
            Assert.AreEqual(secondExpectedTotal, secondReturnedTotal);
        }

        [Test]
        public async Task Test_GetTotalAsync_InvalidId()
        {
            // Arrange
            var cartId = 999;

            // Act
            // Service method returns 0 if cart isn't found.
            var returnedTotal = await cartService.GetTotalAsync(cartId);

            // Assert
            Assert.AreEqual(0, returnedTotal);
        }

        [Test]
        public async Task RemoveProductAsync_ReduceQuantity()
        {
            // Arrange
            // Initial product quantity is 4.
            var expectedQuantity = 3;
            var cartId = 2;
            var productId = 3;
            var cartProducts = context.CartsProducts
                .First(cp => cp.CartId == cartId && cp.ProductId == productId);

            // Act
            await cartService.RemoveProductAsync(cartId, productId);

            // Assert
            Assert.AreEqual(expectedQuantity, cartProducts.ProductQuantity);
        }

        [Test]
        public async Task RemoveProductAsync_RemoveProduct()
        {
            // Arrange
            var cartId = 3;
            var productId = 3;

            // Act
            await cartService.RemoveProductAsync(cartId, productId);
            var cartProducts = context.CartsProducts
                .FirstOrDefault(cp => cp.CartId == cartId && cp.ProductId == productId);

            // Assert
            Assert.IsNull(cartProducts);
        }
        // Not testing invalid case, since nothing happens to the db with invalid parameters.

        [Test]
        public async Task RemoveAllProductsAsync_Valid()
        {
            // Arrange
            var cartId = 3;

            // Act
            await cartService.RemoveAllProductsAsync(cartId);
            var cartProducts = context.CartsProducts.FirstOrDefault(cp => cp.CartId == cartId);

            // Assert
            Assert.IsNull(cartProducts);
        }
        // Not testing invalid, as nothing happens when an invalid cartId is passed.
    }
}