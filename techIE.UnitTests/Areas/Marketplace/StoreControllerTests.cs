namespace techIE.UnitTests.Areas.Marketplace
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Mvc;

    using Moq;
    using NUnit.Framework;

    using Contracts;
    using techIE.Models.Products;
    using techIE.Areas.Marketplace.Controllers;

    using techIE.Models.Categories;

    public class StoreControllerTests
    {
        private StoreController controller;
        private Mock<IProductService> productServiceMock;
        private Mock<ICategoryService> categoryServiceMock;

        [SetUp]
        public void Tests_Initialize()
        {
            productServiceMock = new Mock<IProductService>();
            categoryServiceMock = new Mock<ICategoryService>();

            controller = new StoreController(
                productServiceMock.Object,
                categoryServiceMock.Object);
        }

        [Test]
        public void Test_Index_AlwaysReturnsView()
        {
            // Arrange
            productServiceMock
                .Setup(p => p.GetThreeRandomAsync(It.IsAny<bool>()))
                .ReturnsAsync(new List<ProductOverviewViewModel>());

            // Act
            var action = controller.Index();

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }

        [Test]
        public void Test_Empty_WithCategories()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();
            var category = new CategoryViewModel();

            categories.Add(category);
            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            // Act
            var action = controller.Empty();

            // Assert
            Assert.That(action.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void Test_Empty_WithoutCategories()
        {
            // Arrange
            var categories = new List<CategoryViewModel>();

            categoryServiceMock
                .Setup(c => c.GetAllAvailableAsync())
                .ReturnsAsync(categories);

            // Act
            var action = controller.Empty();

            // Assert
            Assert.That(action.Result, Is.TypeOf<ViewResult>());
        }
    }
}
