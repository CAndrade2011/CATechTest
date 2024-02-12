using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Service;
using Domain.Aggregate;
using Domain.Command;
using Domain.Repository;
using Domain.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.Test.Controllers;

public class ProductControllerTest
{
    private readonly Mock<IProductService> _mockProductService;
    private readonly Mock<ILogger<ProductController>> _mockLogger;
    private const string MOCK_PRODUCT_ID = "0f8fad5bd9cb469fa1657086";

    public ProductControllerTest()
    {
        _mockLogger = new Mock<ILogger<ProductController>>();
        _mockProductService = new Mock<IProductService>();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task GetAllProducts_MustReturnList()
    {
        // Arrange
        var expectedResult = new List<ProductAggregate>() { new ProductAggregate() };
        _mockProductService.Setup(mock => mock.GetAllProductsAsync()).ReturnsAsync(expectedResult);
        var controller = new ProductController(_mockLogger.Object, _mockProductService.Object);

        // Act
        var sut = await controller.GetAllProducts();

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task GetProductById_MustReturnItem()
    {
        // Arrange
        var expectedResult = new ProductAggregate();
        _mockProductService.Setup(mock => mock.GetProductByIdAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);
        var controller = new ProductController(_mockLogger.Object, _mockProductService.Object);

        // Act
        var sut = await controller.GetProductById(MOCK_PRODUCT_ID);

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task CreateProduct_MustReturnTrue()
    {
        // Arrange
        var expectedResult = true;
        _mockProductService.Setup(mock => mock.CreateProductAsync(It.IsAny<ProductAggregate>())).ReturnsAsync(expectedResult);
        var controller = new ProductController(_mockLogger.Object, _mockProductService.Object);

        // Act
        var sut = await controller.CreateProduct(new AddProductCommand());

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task UpdateProduct_MustReturnTrue()
    {
        // Arrange
        var expectedResult = true;
        _mockProductService.Setup(mock => mock.UpdateProductAsync(It.IsAny<ProductAggregate>())).ReturnsAsync(expectedResult);
        var controller = new ProductController(_mockLogger.Object, _mockProductService.Object);

        // Act
        var sut = await controller.UpdateProduct(MOCK_PRODUCT_ID, new UpdProductCommand());

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task DeleteProduct_MustReturnNothing()
    {
        // Arrange
        var expectedResult = true;
        _mockProductService.Setup(mock => mock.DeleteProductAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);
        var controller = new ProductController(_mockLogger.Object, _mockProductService.Object);

        // Act
        var sut = await controller.DeleteProduct(MOCK_PRODUCT_ID);

        // Assert
        sut.Should().NotBeNull();
    }

}
