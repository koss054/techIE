namespace techIE.UnitTests.Areas.Official
{
    using Moq;
    using NUnit.Framework;
    using Microsoft.AspNetCore.Mvc;

    using techIE.Contracts;
    using techIE.Areas.Official.Controllers;
    using techIE.Models;
    using techIE.Models.Products;

    public class ProductControllerTests
    {
        private ProductController controller;
        private Mock<IUserService> userServiceMock;
        private Mock<IProductService> productServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            userServiceMock = new Mock<IUserService>();
            productServiceMock = new Mock<IProductService>();

            controller = new ProductController(
                userServiceMock.Object,
                productServiceMock.Object);
        }

        [Test]
        public void Test_DetailsValid_ReturnsView()
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
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_DetailsNoSeller_ReturnsBadRequest()
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
        public void Test_DetailsNoModel_ReturnsNotFound()
        {
            // Arrange
            ProductDetailedViewModel model = null;
            userServiceMock
                .Setup(u => u.GetUserByProductIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new UserViewModel());

            productServiceMock
                .Setup(p => p.GetDetailedAsync(It.IsAny<int>(), It.IsAny<UserViewModel>()))
                .ReturnsAsync(model);

            // Act
            var action = controller.Details(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_DetailsDeletedModel_ReturnsNotFound()
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
        public void Test_DetailsMarketplaceProduct_ReturmsRedirectToAction()
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
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }
    }
}
