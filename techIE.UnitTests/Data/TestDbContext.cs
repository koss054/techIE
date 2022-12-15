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
            SeedCategories();
            SeedCarts();
            SeedOrders();
            SeedProducts();
            SeedCartProducts();
            SeedUserProducts();
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

        private void SeedCategories()
        {
            var categories = new List<Category>()
            {
                new Category(){ Id = 1, Name = "Phones", IsOfficial = true, IsDeleted = false },
                new Category(){ Id = 2, Name = "Computers", IsOfficial = false, IsDeleted = false },
                new Category(){ Id = 3, Name = "Fax Machines", IsOfficial = false, IsDeleted = true }
            };

            context.AddRange(categories);
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
                new Cart(){ Id = 3, IsCurrent = false, UserId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", OrderId = 3 },
                new Cart(){ Id = 4, IsCurrent = true, UserId = "sse3f072-d231-e1e1-ab26-1120hhj364e4" }
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
                new Product(){ Id = 1, Name = "Nokia 3310", Price = 100, ImageUrl = "www.com", Description = "A classic, unbreakable phone.", IsOfficial = true, CategoryId = 1, IsDeleted = false },
                new Product(){ Id = 2, Name = "Qk Laptop", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = true, CategoryId = 2, IsDeleted = true },
                new Product(){ Id = 3, Name = "Umen Chasovnik", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch.",  IsOfficial = false, CategoryId = 3, IsDeleted = false },
                new Product(){ Id = 4, Name = "Deleted Product", Price = 1000, ImageUrl = "www.com", Description = "Deleted product, man.", IsOfficial = false, CategoryId = 4, IsDeleted = true }
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
                new CartProduct(){ CartId = 4, ProductQuantity = 2, ProductId = 2 },
            };

            context.AddRange(cartProducts);
            context.SaveChanges();
        }

        private void SeedUserProducts()
        {
            var userProducts = new List<UserProduct>()
            {
                new UserProduct(){ UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", ProductId = 1 },
                new UserProduct(){ UserId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b", ProductId = 2},
                new UserProduct(){ UserId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f", ProductId = 3}
            };

            context.AddRange(userProducts);
            context.SaveChanges();
        }
    }
}
