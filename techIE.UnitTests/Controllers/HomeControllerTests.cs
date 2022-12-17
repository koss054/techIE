namespace techIE.UnitTests.Controllers
{
    using NUnit.Framework;
    using Microsoft.AspNetCore.Mvc;

    using techIE.Controllers;

    public class HomeControllerTests
    {
        private HomeController controller;

        [SetUp]
        public void Tests_Initialize()
        {
            controller = new HomeController();
        }

        [Test]
        public void Test_PagesReturning_ViewResult()
        {
            // Assert

            // Act
            var indexResult = controller.Index();
            var storyResult = controller.Story();
            var missionResult = controller.Mission();

            // Arrange
            Assert.That(indexResult, Is.TypeOf<ViewResult>());
            Assert.That(storyResult, Is.TypeOf<ViewResult>());
            Assert.That(missionResult, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_Error400_ViewResultName()
        {
            // Assert
            var expectedViewName = "Error400";
            var statusCode = 400;

            // Act
            var result = controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Test_Error404_ViewResultName()
        {
            // Assert
            var expectedViewName = "Error400";
            var statusCode = 404;

            // Act
            var result = controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Test_Error401_ViewResultName()
        {
            // Assert
            var expectedViewName = "Error401";
            var statusCode = 401;

            // Act
            var result = controller.Error(statusCode) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Test_AnyOtherError_ViewResultName()
        {
            // Assert
            var expectedViewName = "Error";
            var statusCode = 402;

            // Act
            var result = controller.Error(statusCode);

            // Assert
            Assert.That(result, Is.TypeOf<ViewResult>());
        }
    }
}
