#nullable disable
namespace techIE.UnitTests.Controllers
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Moq;
    using NUnit.Framework;

    using Contracts;

    using techIE.UnitTests.TestControllers;
    using techIE.Models.Orders;

    public class OrderControllerTests
    {
        // Using a new orderController, because the static ClaimsPrincipalExtensions User.Id() method doesn't allow the tests to run.
        private OrderTestController controller;
        Mock<ICartService> cartServiceMock;
        Mock<IOrderService> orderServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            cartServiceMock = new Mock<ICartService>();
            orderServiceMock = new Mock<IOrderService>();

            controller = new OrderTestController(
                cartServiceMock.Object,
                orderServiceMock.Object);
        }

        [Test]
        public void Test_FinishOrder_WithCurrentUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var action = controller.Finish(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<RedirectToActionResult>());
        }

        [Test]
        public void Test_FinishOrder_WithIncorrectUser()
        {
            // Arrange
            cartServiceMock
                .Setup(c => c.IsCartForUserAsync(It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act
            var action = controller.Finish(It.IsAny<int>());

            // Assert
            Assert.That(action.Result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public void Test_GetOrderHistory_AlwaysReturnsView()
        {
            // Arrange
            orderServiceMock
                .Setup(o => o.GetHistoryAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<OrderHistoryViewModel>());

            // Act
            var action = controller.History();

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }
    }
}
