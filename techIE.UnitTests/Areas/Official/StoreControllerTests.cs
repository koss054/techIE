namespace techIE.UnitTests.Areas.Official
{
    using Microsoft.AspNetCore.Mvc;

    using Moq;
    using NUnit.Framework;

    using Contracts;
    using techIE.Areas.Official.Controllers;

    public class StoreControllerTests
    {
        private StoreController controller;
        private Mock<IProductService> productServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            productServiceMock = new Mock<IProductService>();

            controller = new StoreController(productServiceMock.Object);
        }

        [Test]
        public void Test_Pages_AlwaysReturnView()
        {
            // Arrange

            // Act
            var indexAction = controller.Index();
            var phonesAction = controller.Phones();
            var laptopAction = controller.Laptops();
            var smartAction = controller.Smart();

            // Assert
            Assert.That(indexAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(phonesAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(laptopAction.Result, Is.TypeOf<ViewResult>());
            Assert.That(smartAction.Result, Is.TypeOf<ViewResult>());
        }
    }
}
