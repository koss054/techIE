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
    using techIE.Data.Entities;

    public class CategoryControllerTests
    {
        private CategoryTestController controller;
        private Mock<ICategoryService> categoryServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            categoryServiceMock = new Mock<ICategoryService>();

            controller = new CategoryTestController(categoryServiceMock.Object);
        }

        [Test]
        public void Test_AddUserIsAdmin_Valid()
        {
            // Arrange
            var isUserAdmin = true;
            
            // Act
            var getResult = controller.Add(isUserAdmin);
            var postAction = controller.Add(new CategoryFormViewModel(), isUserAdmin);

            // Assert
            Assert.That(getResult, Is.TypeOf<ViewResult>());
            Assert.That(postAction.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_AddUserNotAdmin_ReturnsUnauthorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var result = controller.Add(isUserAdmin);

            // Assert
            Assert.That(result, Is.TypeOf<UnauthorizedResult>());
        }

        [Test]
        public void Test_EditUserIsAdmin_Valid()
        {
            // Arrange
            var isUserAdmin = true;

            categoryServiceMock
                .Setup(c => c.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(new Category());

            // Act
            var getAction = controller.Edit(It.IsAny<int>(), isUserAdmin);
            var postAction = controller.Edit(It.IsAny<CategoryFormViewModel>(), isUserAdmin);

            // Assert
            Assert.That(getAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(postAction.Result, Is.TypeOf<RedirectToActionResult>());
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
        public void Test_EditCategoryEntityNull_ReturnsNotFound()
        {
            // Arrange
            var isUserAdmin = true;
            Category category = null;

            categoryServiceMock
                .Setup(c => c.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(category);

            // Act
            var action = controller.Edit(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_VerifyUserIsAdmin_ReturnsRedirectToAction()
        {
            // Arrange
            var isUserAdmin = true;

            // Act
            var action = controller.Verify(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_VerifyUserNotAdmin_ReturnsUnathorized()
        {
            // Arrange
            var isUserAdmin = false;

            // Act
            var action = controller.Verify(It.IsAny<int>(), isUserAdmin);

            // Assert
            Assert.That(action.Result, Is.TypeOf<UnauthorizedResult>());
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
        public void Test_DeleteUserNotAdmin_ReturnsUnathorized()
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
        public void Test_RestoreUserNotAdmin_ReturnsUnathorized()
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
