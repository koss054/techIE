namespace techIE.UnitTests.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using NUnit.Framework;

    using techIE.Services;
    using techIE.Contracts;
    using techIE.Data;
    using techIE.Data.Entities;
    using techIE.Data.Entities.Enums;
    using techIE.Models;
    using techIE.Models.Products;

    using techIE.UnitTests.Data;

    [TestFixture]
    public class ProductServiceTests
    {
        private AppDbContext context;
        private IProductService productService;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();

            productService = new ProductService(context);
        }

        [Test]
        public async Task Test_GetAsync_ValidId()
        {
            var productId = 1;
            var expectedProduct = context.Products.FirstOrDefault(p => p.Id == productId);

            var product = await productService.GetAsync(productId);

            Assert.AreEqual(expectedProduct, product);
        }

        [Test]
        public async Task Test_GetAsync_InvalidId()
        {
            var productId = 999;

            var product = await productService.GetAsync(productId);

            Assert.AreEqual(null, product);
        }

        [Test]
        public async Task Test_GetDetailedAsync_WithAvailable()
        {
            var productId = 1;
            var seller = new UserViewModel() { Id = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b" };
            var expectedProduct = await productService.GetAsync(productId);

            var product = await productService.GetDetailedAsync(productId, seller);

            Assert.IsNotNull(expectedProduct);
            Assert.IsNotNull(product);
            Assert.AreEqual(expectedProduct.Name, product.Name);
        }

        [Test]
        public async Task Test_GetDetailedAsync_WithDeleted()
        {
            // Product with Id 4 is deleted in our test database.
            var productId = 4;
            var seller = new UserViewModel() { Id = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b" };

            var product = await productService.GetDetailedAsync(productId, seller);

            Assert.IsNull(product);
        }

        [Test]
        public async Task Test_GetAllAdminAsync()
        {
            // Expected count is 2, since we have 2 products with IsOfficial == true in test db.
            var expectedProductCount = 2;

            var products = await productService.GetAllAdminAsync();

            Assert.AreEqual(expectedProductCount, products.Count());
        }

        [Test]
        public async Task Test_GetAllAsync_WithIsOfficialTrue()
        {
            // Expected count is 1, since we have 1 product with IsOfficial == true in test db that isn't deleted.
            var expectedProductCount = 1;
            var expectedProductName = "Nokia 3310";

            var products = await productService.GetAllAsync(true);

            Assert.AreEqual(expectedProductCount, products.Count());
            Assert.AreEqual(expectedProductName, products.First().Name);
        }

        [Test]
        public async Task Test_GetAllAsync_WithIsOfficialFalse()
        {
            // Expected count is 1, since we have 1 product with IsOfficil == false in test db that isn't deleted.
            var expectedProductCount = 1;
            var expectedProductName = "Umen Chasovnik";

            var products = await productService.GetAllAsync(false);

            Assert.AreEqual(expectedProductCount, products.Count());
            Assert.AreEqual(expectedProductName, products.First().Name);
        }

        [Test]
        public async Task Test_GetFormModelAsync_ValidId()
        {
            var productId = 2;
            var expectedProductName = "Qk Laptop";

            var product = await productService.GetFormModelAsync(productId);

            Assert.IsNotNull(product);
            Assert.AreEqual(expectedProductName, product.Name);
        }

        [Test]
        public async Task Test_GetFormModelAsync_InvalidId()
        {
            var productId = 999;

            var product = await productService.GetFormModelAsync(productId);

            Assert.IsNull(product);
        }

        [Test]
        public async Task Test_GetCurrentUserProductsAsync()
        {
            // User with this Id is selling 2 products in our test db.
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";
            var expectedProductCount = 2;

            var products = await productService.GetCurrentUserProductsAsync(userId);

            Assert.IsNotNull(products);
            Assert.AreEqual(expectedProductCount, products.Count());
        }

        [Test]
        public async Task Test_GetThreeRandomAsync_WithOneOfficialProduct()
        {
            // Currently, our test db has only 1 product that isn't deleted and is official.
            var expectedProductCount = 1;

            var products = await productService.GetThreeRandomAsync(true);

            Assert.IsNotNull(products);
            Assert.AreEqual(expectedProductCount, products.Count());
        }

        [Test]
        public async Task Test_GetThreeRandomAsync_WithThreeUnofficial()
        {
            // Currently, our test db has only 1 product that isn't deleted and is unofficial.
            // We'll manually add 3 more products so we can test the functionality.
            // The expected count will be 3, despite having 4 elidgible products, as the method takes only 3 random products.
            var expectedProductCount = 3;
            var manualProducts = new List<Product>()
            {
                 new Product(){ Id = 10, Name = "Qk Laptop 22", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = false, CategoryId = 2, IsDeleted = false },
                new Product(){ Id = 20, Name = "Umen Chasovnikar", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch.",  IsOfficial = false, CategoryId = 3, IsDeleted = false },
                new Product(){ Id = 30, Name = "Not a Deleted Product", Price = 1000, ImageUrl = "www.com", Description = "Deleted product, man.", IsOfficial = false, CategoryId = 2, IsDeleted = false }
            };

            context.AddRange(manualProducts);
            context.SaveChanges();

            var products = await productService.GetThreeRandomAsync(false);

            Assert.IsNotNull(products);
            Assert.AreEqual(expectedProductCount, products.Count());
        }

        [Test]
        public async Task Test_GetSearchResultAsync_Newest()
        {
            // Name of the product that is the most "recently added" one in our test db, that isn't deleted and is unofficial.
            var expectedProductName = "Umen Chasovnik";

            var searchResult = await productService.GetSearchResultAsync(null, null, ProductSorting.Newest, 1, 3);
            var newestProduct = searchResult.Products.First();

            Assert.IsNotNull(newestProduct);
            Assert.AreEqual(expectedProductName, newestProduct.Name);
        }

        [Test]
        public async Task Test_GetSearchResultAsync_Price()
        {
            // 50 will be the price of the cheapest, not deleted, available product in the test db.
            var expectedProductPrice = 50;
            var newProduct = new Product() { Id = 10, Name = "Kofti Laptop", Price = 50, ImageUrl = "www.com", Description = "Very uncool, indeed.", IsOfficial = false, CategoryId = 2, IsDeleted = false };

            context.Add(newProduct);
            context.SaveChanges();

            var searchResult = await productService.GetSearchResultAsync(null, null, ProductSorting.Price, 1, 3);
            var cheapestProduct = searchResult.Products.First();

            Assert.IsNotNull(cheapestProduct);
            Assert.AreEqual(expectedProductPrice, cheapestProduct.Price);
        }

        [Test]
        public async Task Test_GetSearchResultAsync_Name()
        {
            // Id 98 will be the Id of the product that is the first when sorting alphabetically.
            var expectedProductId = 98;
            var newProducts = new List<Product>()
            {
                new Product(){ Id = 54, Name = "Qk Minecraft Laptop 64", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = false, CategoryId = 2, IsDeleted = false },
                new Product(){ Id = 98, Name = "Amazing Chasovnikar", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch.",  IsOfficial = false, CategoryId = 3, IsDeleted = false }
            };

            context.AddRange(newProducts);
            context.SaveChanges();

            var searchResult = await productService.GetSearchResultAsync(null, null, ProductSorting.Alphabetical, 1, 3);
            var alphabeticalProduct = searchResult.Products.First();

            Assert.IsNotNull(alphabeticalProduct);
            Assert.AreEqual(expectedProductId, alphabeticalProduct.Id);
        }

        [Test]
        public async Task Test_GetSearchResultAsync_SearchTerm()
        {
            // Addning new products specific to this search term test.
            // Expected count is 2, despite having a product in the test db with "indeed" in its description - that product is official and deleted.
            var searchTerm = "indEeD";
            var expectedProductCount = 2;
            var newProducts = new List<Product>()
            {
                new Product(){ Id = 54, Name = "Qk Minecraft Laptop 64", Price = 2000, ImageUrl = "www.com", Description = "Very cool, indeed.", IsOfficial = false, CategoryId = 2, IsDeleted = false },
                new Product(){ Id = 98, Name = "Amazing Chasovnikar", Price = 300,ImageUrl = "www.com", Description = "Smart. Watch. INDEED",  IsOfficial = false, CategoryId = 3, IsDeleted = false }
            };

            context.AddRange(newProducts);
            context.SaveChanges();

            var searchResult = await productService.GetSearchResultAsync(null, searchTerm, ProductSorting.Newest, 1, 30);
            var productCount = searchResult.Products.Count();

            Assert.AreEqual(expectedProductCount, productCount);
        }

        [Test]
        public async Task Test_GetSearchResultAsync_Category()
        {
            // Only 1 product in our test db with the category "Fax Machines".
            // The Id of the product with the searched category name is 3.
            var category = "Fax Machines";
            var expectedProductCount = 1;
            var expectedProductId = 3;

            var searchResult = await productService.GetSearchResultAsync(category, null, ProductSorting.Newest, 1, 20);
            var productCount = searchResult.Products.Count();
            var productWithCategory = searchResult.Products.First();

            Assert.IsNotNull(productWithCategory);
            Assert.AreEqual(expectedProductCount, productCount);
            Assert.AreEqual(category, productWithCategory.Category);
            Assert.AreEqual(expectedProductId, productWithCategory.Id);
        }

        [Test]
        public async Task Test_GetSearchResultsAsync_CategoryAndSearchTerm()
        {
            // Only 1 product will be returned, despite 2 being in the provided category.
            // Only one of these products will have the provided search term.
            var searchTerm = "InDEEd";
            var category = "Fax Machines";
            var expectedProductCount = 1;
            var expectedProductId = 64;

            var newProduct = new Product(){ Id = 64, Name = "Netherrite e shano za pisane", Price = 50, ImageUrl = "www.com", Description = "Very difficult, indeed.", IsOfficial = false, CategoryId = 3, IsDeleted = false };

            context.Add(newProduct);
            context.SaveChanges();

            var searchResult = await productService.GetSearchResultAsync(category, searchTerm, ProductSorting.Newest, 1, 20);
            var productCount = searchResult.Products.Count();
            var productWithCategoryAndTerm = searchResult.Products.First();

            Assert.IsNotNull(productWithCategoryAndTerm);
            Assert.AreEqual(expectedProductCount, productCount);
            Assert.AreEqual(category, productWithCategoryAndTerm.Category);
            Assert.AreEqual(expectedProductId, productWithCategoryAndTerm.Id);
        }

        [Test]
        public async Task Test_AddAsync()
        {
            // Product and user are also mapped in UserProduct table.
            // Prior to adding, we have only 3 UserProduct entries in our test db.
            var initialUserProductCount = 3;

            var productModel = new ProductFormViewModel(){ Name = "Hmmmmm", Price = 50, ImageUrl = "www.com", Description = "Very hmmmm, indeed.", IsOfficial = false, CategoryId = 3 };
            var sellerId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";

            await productService.AddAsync(productModel, sellerId);

            var newProduct = context.Products.Last();
            var newUserProductCount = context.UsersProducts.ToList().Count();

            Assert.IsNotNull(newProduct);
            Assert.AreNotEqual(initialUserProductCount, newUserProductCount);
        }

        [Test]
        public async Task Test_EditAsync()
        {
            // Id of product that we are editing is 3.
            var dbProduct = await productService.GetAsync(3);
            var productModel = new ProductFormViewModel() { Id = 3, Name = "Hmmmmm", Price = 50, ImageUrl = "www.com", Description = "Very hmmmm, indeed.", IsOfficial = false, CategoryId = 3 };

            await productService.EditAsync(productModel);

            Assert.IsNotNull(dbProduct);
            Assert.IsNotNull(productModel);
            Assert.AreEqual(dbProduct.Name, productModel.Name);
        }

        [Test]
        public async Task Test_DeleteAsync()
        {
            // Id of product that we are deleting is 3, since it's available.
            var dbProduct = await productService.GetAsync(3);

            await productService.DeleteAsync(3);

            Assert.IsNotNull(dbProduct);
            Assert.IsTrue(dbProduct.IsDeleted);
        }

        [Test]
        public async Task Test_RestoreAsync()
        {
            // Id of product that we are restoring is 2, since it's deleted.
            var dbProduct = await productService.GetAsync(2);

            await productService.RestoreAsync(2);

            Assert.IsNotNull(dbProduct);
            Assert.IsFalse(dbProduct.IsDeleted);
        }

        [Test]
        public async Task Test_IsUserSellerAsync_True()
        {
            var productId = 1;
            var userId = "a9ad02b6-f60f-4bae-b99a-83fbacbb0c9b";

            var result = await productService.IsUserSellerAsync(productId, userId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task Test_IsUserSellerAsync_False()
        {
            var productId = 1;
            var userId = "de505807-eafb-4f1f-a7cb-51cb2d88d80f";

            var result = await productService.IsUserSellerAsync(productId, userId);

            Assert.IsFalse(result);
        }
    }
}
