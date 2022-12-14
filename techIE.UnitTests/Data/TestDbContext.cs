namespace techIE.UnitTests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using techIE.Data.Entities;
    using techIE.Data;

    public class TestDbContext
    {
        private AppDbContext context;

        /// <summary>
        /// Create a test database for the unit tests.
        /// Seeding the information with the private methods below the constructor.
        /// </summary>
        public TestDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"ApplicationDatabase{Guid.NewGuid()}")
                .Options;

            context = new AppDbContext(options);

            SeedUsers();
            SeedCarts();
            SeedOrders();
            SeedProducts();
            SeedCartProducts();
        }

        public AppDbContext GetSeededEntities()
        {
            return context;
        }

        /// <summary>
        /// Seed the users.
        /// </summary>
        private void SeedUsers()
        {
            var users = new List<User>()
            {
                new User(){ Id = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", UserName = "firstUser", Email = "first@email.com" },
                new User(){ Id = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", UserName = "secondUser", Email = "second@email.com" },
                new User(){ Id = "sse3f072-d231-e1e1-ab26-1120hhj364e4", UserName = "thirdUser", Email = "third@email.com" }
            };

            context.AddRange(users);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the carts.
        /// </summary>
        private void SeedCarts()
        {
            var carts = new List<Cart>()
            {
                new Cart(){ Id = 1, IsCurrent = true, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", OrderId = 1 },
                new Cart(){ Id = 2, IsCurrent = false, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", OrderId = 2 },
                new Cart(){ Id = 3, IsCurrent = false, UserId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", OrderId = 3 }
            };

            context.AddRange(carts);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the orders.
        /// </summary>
        private void SeedOrders()
        {
            var orders = new List<Order>()
            {
                new Order(){ Id = 1, TotalValue = 2000, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", CartId = 1 },
                new Order(){ Id = 2, TotalValue = 1200, UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", CartId = 2},
                new Order(){ Id = 3, TotalValue = 800, UserId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", CartId = 3}
            };

            context.AddRange(orders);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the products.
        /// </summary>
        private void SeedProducts()
        {
            var products = new List<Product>()
            {
                new Product(){ Id = 1, Name = "Nokia 3310", Price = 100, ImageUrl = "www.com", Description = "A classic, unbreakable phone.", IsOfficial = true, CategoryId = 1 },
                new Product(){ Id = 2, Name = "Qk Laptop", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = false, CategoryId = 2 },
                new Product(){ Id = 3, Name = "Umen Chasovnik", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch.",  IsOfficial = false, CategoryId = 3 }
            };

            context.AddRange(products);
            context.SaveChanges();
        }

        /// <summary>
        /// Seed the mapping cart products.
        /// </summary>
        private void SeedCartProducts()
        {
            var cartProducts = new List<CartProduct>()
            {
                new CartProduct(){ CartId = 1, ProductQuantity = 1, ProductId = 2 },
                new CartProduct(){ CartId = 2, ProductQuantity = 4, ProductId = 3 },
                new CartProduct(){ CartId = 3, ProductQuantity = 1, ProductId = 3 },
                new CartProduct(){ CartId = 3, ProductQuantity = 5, ProductId = 1 },
            };

            context.AddRange(cartProducts);
            context.SaveChanges();
        }
    }
}
