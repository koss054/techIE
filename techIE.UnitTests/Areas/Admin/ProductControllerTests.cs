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

    public class ProductControllerTests
    {
        private ProductTestController controller;
        private Mock<IProductService> productServiceMock;
        private Mock<ICategoryService> categoryServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            productServiceMock = new Mock<IProductService>();
            categoryServiceMock = new Mock<ICategoryService>();

            controller = new ProductTestController(
                productServiceMock.Object,
                categoryServiceMock.Object);
        }

        [Test]
        public void Test_AddValid_ReturnsView()
        {
            // Arrange
            var isUserAdmin = true;
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel() { IsOfficial = true };

            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(categories);

            // Act
            var getAction = controller.Add(isUserAdmin);
            var postAction = controller.Add(new ProductFormViewModel(), isUserAdmin);

            // Assert
            Assert.That(getAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(postAction.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_AddUserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Add(isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_AddNoCategoriesAvailable_ReturnsRedirectToAction()
        {
            // Arrange
            var isUserAdmin = true;

            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(new List<CategoryViewModel>());

            // Act
            var action = controller.Add(isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_EditValid_ReturnsView()
        {
            // Arrange
            var isUserAdmin = true;
            var model = new ProductFormViewModel();
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel() { IsOfficial = true };

            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(categories);

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Edit(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_EditUserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Edit(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_EditNullProductModel_ReturnsNotFound()
        {
            // Arrange
            var isUserAdmin = true;
            ProductFormViewModel model = null;

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Edit(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_EditNoCategoriesAvailable_ReturnsRedirectToAction()
        {
            // Arrange
            var isUserAdmin = true;
            var model = new ProductFormViewModel();

            categoryServiceMock
                .Setup(c => c.GetOfficialAsync())
                .ReturnsAsync(new List<CategoryViewModel>());

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Edit(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_DeleteUserIsAdmin_ReturnsRedirectToAction()
        {
            // Arrange
            var isUserAdmin = true;

            // Act
            var action = controller.Delete(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_DeleteUserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Delete(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_RestoreUserIsAdmin_ReturnsRedirectToAction()
        {
            // Arrange
            var isUserAdmin = true;

            // Act
            var action = controller.Restore(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_RestoreUserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Restore(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
        }
    }
}
