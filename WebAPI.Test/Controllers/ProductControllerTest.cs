using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Service;
using Domain.Aggregate;
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

}
