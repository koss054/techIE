namespace techIE.UnitTests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using NUnit.Framework;

    using techIE.Data;
    using techIE.Contracts;
    using techIE.Services;
    using techIE.Models.Categories;

    using techIE.UnitTests.Data;

    [TestFixture]
    public class CategoryServiceTests
    {
        private AppDbContext context;
        private ICategoryService categoryService;

        [SetUp]
        public void TestInitialize()
        {
            var testContext = new TestDbContext();
            context = testContext.GetSeededEntities();

            categoryService = new CategoryService(context);
        }

        [Test]
        public async Task Test_GetAsync_ValidId()
        {
            var categoryId = 2;
            var expectedCategoryName = "Computers";

            var category = await categoryService.GetAsync(categoryId);

            Assert.AreEqual(expectedCategoryName, category.Name);
        }

        [Test]
        public async Task Test_GetAsync_InvalidId()
        {
            var categoryId = 999;

            var category = await categoryService.GetAsync(categoryId);

            Assert.AreEqual(null, category);
        }

        [Test]
        public async Task Test_GetAllNamesAsync()
        {
            // Expected names are only 2, since the method only accounts for categories with IsDeleted == false.
            var expectedNameCount = 2;

            var categoryNames = await categoryService.GetAllNamesAsync();

            Assert.AreEqual(expectedNameCount, categoryNames.Count());
        }

        [Test]
        public async Task Test_GetAllAsync()
        {
            // Expected categories are 3, since this method gets all categories in db.
            // The properties IsOFficial and IsDelted are not accounted for.
            var expectedCategoryCount = 3;

            var categories = await categoryService.GetAllAsync();

            Assert.AreEqual(expectedCategoryCount, categories.Count());
        }

        [Test]
        public async Task Test_GetAllAvailableAsync()
        {
            // Expected categories are 2, since this method only gets categories with IsDeleted = false.
            var expectedCategoryCount = 2;

            var categories = await categoryService.GetAllAvailableAsync();

            Assert.AreEqual(expectedCategoryCount, categories.Count());
        }

        [Test]
        public async Task Test_GetOfficialAsync()
        {
            // Expected count is 1, since only 1 category has IsOfficial == true in our test db.
            var expectedCategoryCount = 1;

            var categories = await categoryService.GetOfficialAsync();

            Assert.AreEqual(expectedCategoryCount, categories.Count());
        }

        [Test]
        public async Task Test_AddAsync()
        {
            var categoryId = 4;
            var model = new CategoryFormViewModel() { Id = categoryId, Name = "Wow", IsOfficial = false };

            await categoryService.AddAsync(model);

            var dbCategory = await categoryService.GetAsync(categoryId);

            Assert.AreNotEqual(null, dbCategory);
            Assert.AreEqual(dbCategory.Id, model.Id);
            Assert.AreEqual(dbCategory.Name, model.Name);
            Assert.AreEqual(dbCategory.IsOfficial, model.IsOfficial);
        }

        [Test]
        public async Task Test_EditAsync_ValidId()
        {
            var categoryId = 3;
            var dbCategory = await categoryService.GetAsync(categoryId);
            var model = new CategoryFormViewModel() { Id = categoryId, Name = "Printers", IsOfficial = false };

            await categoryService.EditAsync(model);

            Assert.AreNotEqual(null, dbCategory);
            Assert.AreEqual(dbCategory.Id, model.Id);
            Assert.AreEqual(dbCategory.Name, model.Name);
            Assert.AreEqual(dbCategory.IsOfficial, model.IsOfficial);
        }
        // Not testing the invalid case, since nothing happens if the user tries to edit a category that isn't in the db.

        [Test]
        public async Task Test_VerifyAsync_WithOfficial()
        {
            var expectedCategoryStatus = false;

            // Category Id is 1, since the category in our test db with IsOfficial == true is with this Id.
            var categoryId = 1;
            var category = await categoryService.GetAsync(categoryId);

            await categoryService.VerifyAsync(categoryId);

            Assert.AreEqual(expectedCategoryStatus, category.IsOfficial);
        }

        [Test]
        public async Task Test_VerifyAsync_WithUnofficial()
        {
            var expectedCategoryStatus = true;

            // Category Id is 2, since it's a category with IsOfficial == false.
            var categoryId = 2;
            var category = await categoryService.GetAsync(categoryId);

            await categoryService.VerifyAsync(categoryId);

            Assert.AreEqual(expectedCategoryStatus, category.IsOfficial);
        }

        [Test]
        public async Task Test_DeleteAsync()
        {
            var expectedCategoryIsDeletedProp = true;

            // Category Id is 2, since this category isn't a deleted one, yet.
            var categoryId = 2;
            var category = await categoryService.GetAsync(categoryId);

            await categoryService.DeleteAsync(categoryId);

            Assert.AreEqual(expectedCategoryIsDeletedProp, category.IsDeleted);
        }

        [Test]
        public async Task Test_RestoreAsync()
        {
            var expectedCategoryIsDeletedProp = false;

            // Category Id is 3, since this category is a deleted one, but not for long.
            var categoryId = 3;
            var category = await categoryService.GetAsync(categoryId);

            await categoryService.RestoreAsync(categoryId);

            Assert.AreEqual(expectedCategoryIsDeletedProp, category.IsDeleted);
        }
    }
}
