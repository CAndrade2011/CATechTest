using BusinessLogicLayer.Service;
using Domain.Aggregate;
using Domain.Repository;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BusinessLogicLayer.Test.Service;

public class UniqueAccountServiceTest
{
    private readonly Mock<IUniqueAccountRepository> _mockUniqueAccountRepository;
    public UniqueAccountServiceTest()
    {
        _mockUniqueAccountRepository = new Mock<IUniqueAccountRepository>();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task GetUniqueAccount_WithValidEmailAndPassword_MustReturnWithData()
    {
        // Arrange
        var expectedResult = new List<UniqueAccountAggregate>() { new UniqueAccountAggregate() };
        _mockUniqueAccountRepository.Setup(mock => mock.GetAllUniqueAccountsAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(expectedResult);
        var service = new UniqueAccountService(_mockUniqueAccountRepository.Object);
        var query = new Domain.Query.GetUniqueAccountQuery { Email = "teste@domain", Password = "password" };

        // Act
        var sut = await service.GetUniqueAccountQueryHandler(query);

        // Assert
        sut.Should().NotBeNull();
        sut.Should().BeOfType<UniqueAccountAggregate>();
    }
}
