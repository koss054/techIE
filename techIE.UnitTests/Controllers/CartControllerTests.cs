namespace techIE.UnitTests.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

    using Moq;
    using NUnit.Framework;

    using Constants;
    using Contracts;
    using Models.Carts;
    using techIE.Data.Entities.Enums;

    using techIE.UnitTests.TestControllers;

    public class CartControllerTests
    {
        // Using a new cartController, because the static ClaimsPrincipalExtensions User.Id() method doesn't allow the tests to run.
        private CartTestController controller;
        private TempDataDictionary tempData;
        private Mock<ICartService> cartServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            cartServiceMock = new Mock<ICartService>();

            controller = new CartTestController(cartServiceMock.Object);

            tempData = new TempDataDictionary(
                new DefaultHttpContext(),
                Mock.Of<ITempDataProvider>());
        }

        [Test]
        public void Test_AddProductToCart_ReturnsSuccessful()
        {
            // Arrange
            tempData["Cart"] = Messages.CartActionSuccessful;
            controller.TempData = tempData;

            cartServiceMock
                .Setup(c => c.AddProductAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(CartAction.Successful);

            // Act
            var action = controller.Add(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_AddProductToCart_ReturnsDuplicate()
        {
            // Arrange
            tempData["Cart"] = Messages.CartActionDuplicate;
            controller.TempData = tempData;

            cartServiceMock
                .Setup(c => c.AddProductAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(CartAction.Duplicate);

            // Act
            var action = controller.Add(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_AddProductToCart_ReturnsFailed()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.AddProductAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(CartAction.Failed);

            // Act
            var action = controller.Add(It.IsAny<int>(), It.IsAny<string>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Test_InspectCart_AlwaysReturnsView()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.GetCurrentCartAsync(It.IsAny<string>()))
                .ReturnsAsync(new CartViewModel());

            // Act
            var action = controller.Inspect();

            // Arrange
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_RemoveProductFromCart_WithCurrentUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.Remove(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_RemoveProductFromCart_WithIncorrectUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Remove(It.IsAny<int>(), It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_EmptyAllProductsFromCart_WithCurrentUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.Empty(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_EmptyAllProductsFromCart_WithIncorrectUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Empty(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_GetCartHistory_WithCurrentUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.History(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_GetCartHistory_WithIncorrectUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.History(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }
    }
}
