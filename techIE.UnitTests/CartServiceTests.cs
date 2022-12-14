namespace techIE.UnitTests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    using techIE.Contracts;
    using techIE.Data;
    using techIE.Data.Entities;
    using techIE.Services;
    using techIE.Models.Carts;
    using techIE.Data.Entities.Enums;

    [TestFixture]
    public class CartServiceTests
    {
        private IEnumerable<User> users;
        private IEnumerable<Cart> carts;
        private IEnumerable<Product> products;
        private IEnumerable<CartProduct> cartProducts;
        private AppDbContext context;

        #region InitializeTests
        [SetUp]
        public void TestInitialize()
        {
            // Users used in tests.
            this.users = new List<User>()
            {
                new User(){ Id = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", UserName = "firstUser", Email = "first@email.com" },
                new User(){ Id = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", UserName = "secondUser", Email = "second@email.com" },
                new User(){ Id = "sse3f072-d231-e1e1-ab26-1120hhj364e4", UserName = "thirdUser", Email = "third@email.com" }
            };

            // Carts used in tests.
            this.carts = new List<Cart>()
            {
                new Cart(){ Id = 1, IsCurrent = true, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", OrderId = 1 },
                new Cart(){ Id = 2, IsCurrent = false, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", OrderId = 2 },
                new Cart(){ Id = 3, IsCurrent = false, UserId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", OrderId = 3 }
            };

            // Products used in tests.
            this.products = new List<Product>()
            {
                new Product(){ Id = 1, Name = "Nokia 3310", Price = 100, ImageUrl = "www.com", Description = "A classic, unbreakable phone.", IsOfficial = true, CategoryId = 1 },
                new Product(){ Id = 2, Name = "Qk Laptop", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = false, CategoryId = 2 },
                new Product(){ Id = 3, Name = "Umen Chasovnik", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch.",  IsOfficial = false, CategoryId = 3 }
            };

            // Cart products used in tests.
            this.cartProducts = new List<CartProduct>()
            {
                new CartProduct(){ CartId = 1, ProductQuantity = 1, ProductId = 2 },
                new CartProduct(){ CartId = 2, ProductQuantity = 4, ProductId = 3 },
                new CartProduct(){ CartId = 3, ProductQuantity = 1, ProductId = 3 },
                new CartProduct(){ CartId = 3, ProductQuantity = 5, ProductId = 1 },
            };

            // Database
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDatabase{Guid.NewGuid()}")
                .Options;

            this.context = new AppDbContext(options);

            this.context.AddRange(this.users);
            this.context.AddRange(this.carts);
            this.context.AddRange(this.products);
            this.context.AddRange(this.cartProducts);

            this.context.SaveChanges();
        }
        #endregion

        #region Tests_AddProductAsync
        [Test]
        public async Task Test_AddProductAsync_Successful()
        {
            ICartService service = new CartService(this.context);

            var productId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Successful);
        }

        [Test]
        public async Task Test_AddProductAsync_Duplicate ()
        {
            ICartService service = new CartService(this.context);

            var productId = 2;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Duplicate);
        }

        [Test]
        public async Task Test_AddProductAsync_Failed()
        {
            ICartService service = new CartService(this.context);

            var productId = 4;
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var action = await service.AddProductAsync(productId, userId);

            Assert.True(action == CartAction.Failed);
        }
        #endregion

        #region Tests_IsCartForUserAsync
        [Test]
        public async Task Test_IsCartForUserAsync_True()
        {
            ICartService service = new CartService(this.context);

            var cartId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.True(await service.IsCartForUserAsync(cartId, userId));
        }

        [Test]
        public async Task Test_IsCartForUserAsync_False()
        {
            ICartService service = new CartService(this.context);

            var cartId = 3;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            Assert.False(await service.IsCartForUserAsync(cartId, userId));
        }
        #endregion

        #region Tests_GetCurrentCartAsync
        [Test]
        public async Task Test_GetCurrentCartAsync_Valid()
        {
            ICartService service = new CartService(this.context);

            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var cart = await service.GetCurrentCartAsync(userId);
            var dbCart = this.context.Carts.FirstOrDefault(c => c.UserId == userId && c.IsCurrent);

            Assert.True(cart.Id == dbCart.Id);
            Assert.True(cart.UserId == dbCart.UserId);
            Assert.True(cart.IsCurrent == dbCart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCurrentCartAsync_Invalid()
        {
            ICartService service = new CartService(this.context);

            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";
            var cart = await service.GetCurrentCartAsync(userId);
            // Service method returns empty model if user doesn't have a current cart.
            var cartModel = new CartViewModel();

            Assert.True(cart.Id == cartModel.Id);
            Assert.True(cart.UserId == cartModel.UserId);
            Assert.True(cart.IsCurrent == cartModel.IsCurrent);
        }
        #endregion

        #region Tests_GetCartAsync
        [Test]
        public async Task Test_GetCartAsync_Valid()
        {
            ICartService service = new CartService(this.context);

            var cartId = 1;
            var cart = await service.GetCartAsync(cartId);
            var dbCart = this.carts.ToList().Find(c => c.Id == cartId);

            Assert.True(cart != null);
            Assert.True(cart.UserId == cart.UserId);
            Assert.True(cart.IsCurrent == cart.IsCurrent);
        }

        [Test]
        public async Task Test_GetCartAsync_Invalid()
        {
            ICartService service = new CartService(this.context);

            var cartId = 4;
            var cartModel = new CartViewModel();
            // Service method returns an empty view model if the provided Id doesn't match any cart in the db.
            var cart = await service.GetCartAsync(cartId);

            Assert.True(cart.Id == cartModel.Id);
            Assert.True(cart.IsCurrent == cartModel.IsCurrent);
        }
        #endregion

        #region Tests_GetTotalAsync
        [Test]
        public async Task Test_GetTotalAsync_Valid()
        {
            ICartService service = new CartService(this.context);

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
            ICartService service = new CartService(this.context);

            var cartId = 999;
            // Service method returns 0 if cart isn't found.
            var returnedTotal = await service.GetTotalAsync(cartId);

            Assert.AreEqual(0, returnedTotal);
        }
        #endregion

        #region Tests_RemoveProductAsync
        [Test]
        public async Task RemoveProductAsync_ReduceQuantity()
        {
            ICartService service = new CartService(this.context);

            var cartId = 2;
            var productId = 3;
            // Initial product quantity is 4.
            var expectedQuantity = 3;
            var cartProducts = this.context.CartsProducts
                .First(cp => cp.CartId == cartId && cp.ProductId == productId);

            await service.RemoveProductAsync(cartId, productId);

            Assert.AreEqual(cartProducts.ProductQuantity, expectedQuantity);
        }

        [Test]
        public async Task RemoveProductAsync_RemoveProduct()
        {
            ICartService service = new CartService(this.context);

            var cartId = 3;
            var productId = 3;

            await service.RemoveProductAsync(cartId, productId);

            var cartProducts = this.context.CartsProducts
                .FirstOrDefault(cp => cp.CartId == cartId && cp.ProductId == productId);


            Assert.AreEqual(cartProducts, null);
        }

        // Not testing invalid case, since nothing happens to the db with invalid parameters.
        #endregion

        #region Tests_RemoveAllProductsAsync
        [Test]
        public async Task RemoveAllProductsAsync_Valid()
        {
            ICartService service = new CartService(context);

            var cartId = 3;
            await service.RemoveAllProductsAsync(cartId);
            var cartProducts = this.context.CartsProducts.FirstOrDefault(cp => cp.CartId == cartId);

            Assert.AreEqual(cartProducts, null);
        }

        // Not testing invalid, as nothing happens when an invalid cartId is passed.
        #endregion
    }
}