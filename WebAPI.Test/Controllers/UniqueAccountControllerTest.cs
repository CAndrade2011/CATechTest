using Domain.Aggregate;
using Domain.Command;
using Domain.Service;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.Test.Controllers;

public class UniqueAccountControllerTest
{
    private readonly Mock<IUniqueAccountService> _mockUniqueAccountService;
    private readonly Mock<ILogger<UniqueAccountController>> _mockLogger;
    private const string MOCK_UNIQUE_ACCOUNT_ID = "0f8fad5bd9cb449fa1657086";

    public UniqueAccountControllerTest()
    {
        _mockLogger = new Mock<ILogger<UniqueAccountController>>();
        _mockUniqueAccountService = new Mock<IUniqueAccountService>();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task GetAllUniqueAccounts_MustReturnList()
    {
        // Arrange
        var expectedResult = new List<UniqueAccountAggregate>() { new UniqueAccountAggregate() };
        _mockUniqueAccountService.Setup(mock => mock.GetAllUniqueAccountsAsync()).ReturnsAsync(expectedResult);
        var controller = new UniqueAccountController(_mockLogger.Object, _mockUniqueAccountService.Object);

        // Act
        var sut = await controller.GetAllUniqueAccounts();

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task GetUniqueAccountById_MustReturnItem()
    {
        // Arrange
        var expectedResult = new UniqueAccountAggregate();
        _mockUniqueAccountService.Setup(mock => mock.GetUniqueAccountByIdAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);
        var controller = new UniqueAccountController(_mockLogger.Object, _mockUniqueAccountService.Object);

        // Act
        var sut = await controller.GetUniqueAccountById(MOCK_UNIQUE_ACCOUNT_ID);

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task CreateUniqueAccount_MustReturnTrue()
    {
        // Arrange
        var expectedResult = true;
        _mockUniqueAccountService.Setup(mock => mock.CreateUniqueAccountAsync(It.IsAny<UniqueAccountAggregate>())).ReturnsAsync(expectedResult);
        var controller = new UniqueAccountController(_mockLogger.Object, _mockUniqueAccountService.Object);

        // Act
        var sut = await controller.CreateUniqueAccount(new AddUniqueAccountCommand());

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task UpdateUniqueAccount_MustReturnTrue()
    {
        // Arrange
        var expectedResult = true;
        _mockUniqueAccountService.Setup(mock => mock.UpdateUniqueAccountAsync(It.IsAny<UniqueAccountAggregate>())).ReturnsAsync(expectedResult);
        var controller = new UniqueAccountController(_mockLogger.Object, _mockUniqueAccountService.Object);

        // Act
        var sut = await controller.UpdateUniqueAccount(MOCK_UNIQUE_ACCOUNT_ID, new UpdUniqueAccountCommand());

        // Assert
        sut.Should().NotBeNull();
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task DeleteUniqueAccount_MustReturnNothing()
    {
        // Arrange
        var expectedResult = true;
        _mockUniqueAccountService.Setup(mock => mock.DeleteUniqueAccountAsync(It.IsAny<string>())).ReturnsAsync(expectedResult);
        var controller = new UniqueAccountController(_mockLogger.Object, _mockUniqueAccountService.Object);

        // Act
        var sut = await controller.DeleteUniqueAccount(MOCK_UNIQUE_ACCOUNT_ID);

        // Assert
        sut.Should().NotBeNull();
    }

}
