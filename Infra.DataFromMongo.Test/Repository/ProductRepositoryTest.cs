using Microsoft.Extensions.Configuration;
using Domain.Aggregate;
using Infra.DataFromMongo.Entity;
using Infra.DataFromMongo.Repository;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infra.DataFromMongo.Test.Repository;

public class ProductRepositoryTest
{
    private readonly Mock<IMongoCollection<Product>> _mockCollection;

    public ProductRepositoryTest()
    {
        _mockCollection = new Mock<IMongoCollection<Product>>();
    }

    [Fact]
    public async Task CreateProductAsync_Should_Insert_Product()
    {
        // Arrange
        CancellationToken cancellationToken = new();
        InsertOneOptions options = new();
        var product = new ProductAggregate { Name = "Test Product", BarCode = "123456", Quantity = 10 };
        var entity = new Product { Name = "Test Product", BarCode = "123456", Quantity = 10 };

        _mockCollection.Setup(c => c.InsertOneAsync(entity, options, cancellationToken)).Returns(Task.CompletedTask);

        var repository = new ProductRepository(_mockCollection.Object);

        // Act
        var result = await repository.CreateProductAsync(product);

        // Assert
        Assert.True(result);
    }

}
