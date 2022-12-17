#nullable disable
namespace techIE.UnitTests.Areas.Admin
{
    using System.Collections.Generic;

    using Moq;
    using NUnit.Framework;
    using Microsoft.AspNetCore.Mvc;

    using techIE.Contracts;
    using techIE.Models.Categories;
    using techIE.Models.Products;

    using techIE.UnitTests.TestControllers.Areas.Admin;

    public class PanelControllerTests
    {
        private PanelTestController controller;
        private Mock<ICategoryService> categoryServiceMock;
        private Mock<IProductService> productServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            categoryServiceMock = new Mock<ICategoryService>();
            productServiceMock = new Mock<IProductService>();

            controller = new PanelTestController(
                categoryServiceMock.Object,
                productServiceMock.Object);
        }

        [Test]
        public void Test_PagesWithAdminCheckOnly_Valid_ReturnView()
        {
            // Arrange
            var isUserAdmin = true;

            productServiceMock
                .Setup(p => p.GetAllAdminAsync())
                .ReturnsAsync(new List<ProductAdminPanelViewModel>());

            // Act
            var indexAction = controller.Index(isUserAdmin);
            var categoriesAction = controller.Categories(isUserAdmin);
            var productsAction = controller.Products(isUserAdmin);

            // Assert
            Assert.That(indexAction, Is.TypeOf<ViewResult>());
            Assert.That(categoriesAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(productsAction.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_PagesWithAdminCheckOnly_UserNotAdmin_ReturnUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var indexAction = controller.Index(isUserAdmin);
            var categoriesAction = controller.Categories(isUserAdmin);
            var productsAction = controller.Products(isUserAdmin);

            // Assert
            Assert.That(indexAction, Is.TypeOf<UnauthorizedResult>());
            Assert.That(categoriesAction.Result, Is.TypeOf<UnauthorizedResult>());
            Assert.That(productsAction.Result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_EmptyCategoryList_Valid_ReturnsView()
        {
            // Arrange
            var isUserAdmin = true;

            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(new List<CategoryViewModel>());

            // Act
            var action = controller.Empty(isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_EmptyCategoryList_UserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Empty(isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_EmptyCategoryList_WithCategories_ReturnsBadRequest()
        {
            // Arrange
            var isUserAdmin = true;
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel();

            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(categories);

            // Act
            var action = controller.Empty(isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<BadRequestResult>());
        }
    }
}
