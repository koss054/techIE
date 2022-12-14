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
    public class CategoryServiceTests
    {
        #region InitializeVariables
        private IEnumerable<User> users;
        private IEnumerable<Cart> carts;
        private IEnumerable<Product> products;
        private IEnumerable<CartProduct> cartProducts;
        private AppDbContext context;
        #endregion

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
                new CartProduct(){ CartId = 2, ProductQuantity = 4, ProductId = 3},
                new CartProduct(){ CartId = 3, ProductQuantity = 1, ProductId = 3}
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

        [Test]
        public void Test()
        {
            Assert.True(true);
        }
    }
}