namespace techIE.UnitTests.Areas.Marketplace
{
    using System.Collections.Generic;

    using Moq;
    using NUnit.Framework;
    using Microsoft.AspNetCore.Mvc;

    using techIE.Contracts;
    using techIE.Areas.Marketplace.Controllers;
    using techIE.Models.Categories;
    using techIE.Models;
    using techIE.Models.Products;

    public class ProductControllerTests
    {
        private ProductTestController controller;
        private Mock<IUserService> userServiceMock;
        private Mock<IProductService> productServiceMock;
        private Mock<ICategoryService> categoryServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            userServiceMock = new Mock<IUserService>();
            productServiceMock = new Mock<IProductService>();
            categoryServiceMock = new Mock<ICategoryService>();

            controller = new ProductTestController(
                userServiceMock.Object,
                productServiceMock.Object,
                categoryServiceMock.Object);
        }

        [Test]
        public void Test_AddProductToMarketplace_WithCategories()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel();
            
            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            // Act
            var getAction = controller.Add();
            var postAction = controller.Add(new ProductFormViewModel());

            // Assert
            Assert.That(getAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(postAction.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_AddProductToMarketplace_WithoutCategories()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();

            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            // Act
            var action = controller.Add();

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_ViewProductDetails_ReturnsView()
        {
            // Arrange
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserViewModel());

            productServiceMock
                .Setup(p => p.GetDetailedAsync(It.IsAny<int>(), It.IsAny<UserViewModel>()))
                .ReturnsAsync(new ProductDetailedViewModel() { IsOfficial = false });

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_ViewProductDetails_NoSeller_ReturnsBadRequest()
        {
            // Arrange
            UserViewModel seller = null;
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(seller);

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Test_ViewProductDetails_NoProduct_ReturnsNotFound()
        {
            // Arrange
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserViewModel());

            ProductDetailedViewModel model = null;
            productServiceMock
                .Setup(p => p.GetDetailedAsync(It.IsAny<int>(), It.IsAny<UserViewModel>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_ViewProductDetails_DeletedProduct_ReturnsNotFound()
        {
            // Arrange
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserViewModel());

            productServiceMock
                .Setup(p => p.GetDetailedAsync(It.IsAny<int>(), It.IsAny<UserViewModel>()))
                .ReturnsAsync(new ProductDetailedViewModel() { IsDeleted = true });

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_ViewProductDetails_IsOfficial_ReturnsRedirectToAction()
        {
            // Arrange
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserViewModel());

            productServiceMock
                .Setup(p => p.GetDetailedAsync(It.IsAny<int>(), It.IsAny<UserViewModel>()))
                .ReturnsAsync(new ProductDetailedViewModel() { IsOfficial = true });

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_EditProductDetails_ValidDetails()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel();

            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductFormViewModel());

            // Act
            var getAction = controller.Edit(It.IsAny<int>());
            var postAction = controller.Edit(It.IsAny<ProductFormViewModel>());

            // Assert
            Assert.That(getAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(postAction.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_EditProductDetails_InvalidUser()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Edit(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_EditProductDetails_NullModel()
        {
            // Arrange
            ProductFormViewModel model = null;
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Edit(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_EditProductDetails_WithoutCategories()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();

            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            productServiceMock
                .Setup(p => p.GetFormModelAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductFormViewModel());

            // Act
            var action = controller.Edit(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_DeleteProduct_WithCurrentUser()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.Delete(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_DeleteProduct_WithInvalidUser()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Delete(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_RestoreProduct_WithCurrentUser()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.Restore(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_RestoreProduct_WithInvalidUser()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.IsUserSellerAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Restore(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }
    }
}
