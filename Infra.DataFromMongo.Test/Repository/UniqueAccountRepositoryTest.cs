using Domain.Aggregate;
using Infra.DataFromMongo.Entity;
using Infra.DataFromMongo.Repository;
using MongoDB.Bson;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit;

namespace Infra.DataFromMongo.Test.Repository;

public class UniqueAccountRepositoryTest
{
    private readonly Mock<IMongoCollection<UniqueAccount>> _mockCollection;

    public UniqueAccountRepositoryTest()
    {
        _mockCollection = new Mock<IMongoCollection<UniqueAccount>>();
    }

    [Fact]
    public async Task CreateUniqueAccountAsync_Should_Insert_UniqueAccount()
    {
        // Arrange
        CancellationToken cancellationToken = new();
        InsertOneOptions options = new();
        var uniqueAccount = new UniqueAccountAggregate
        {
            Name = "Test Unique Account",
            DisplayName = "Unique Account",
            Email = "teste@gmail.com",
            IsAdmin = true,
            Password = "123456"
        };
        var entity = new UniqueAccount
        {
            Id = new ObjectId(uniqueAccount.Id),
            Name = "Test Unique Account",
            DisplayName = "Unique Account",
            Email = "teste@gmail.com",
            IsAdmin = true,
            Password = Encoding.UTF8.GetBytes("123456")
        };

        _mockCollection.Setup(c => c.InsertOneAsync(entity, options, cancellationToken)).Returns(Task.CompletedTask);

        var repository = new UniqueAccountRepository(_mockCollection.Object);

        // Act
        var result = await repository.CreateUniqueAccountAsync(uniqueAccount);

        // Assert
        Assert.True(result);
    }
}
