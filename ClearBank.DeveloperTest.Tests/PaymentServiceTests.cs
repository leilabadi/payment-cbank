using AutoFixture;
using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Application.Services;
using ClearBank.DeveloperTest.Application.Types;
using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Services;
using Moq;
using Xunit;

namespace ClearBank.DeveloperTest.Tests;

public class PaymentServiceTests
{
    private const string ValidAccountNumber = "12345678";
    private const string NonExistingAccountNumber = "87654321";
    private const decimal AccountBalance = 100m;
    private const decimal RequestAmount = 30m;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly PaymentService _sut;
    private readonly Account _account;
    private readonly MakePaymentRequest _request;

    public PaymentServiceTests()
    {
        var fixture = new Fixture();

        _account = fixture.Build<Account>()
            .With(a => a.AccountNumber, ValidAccountNumber)
            .With(a => a.Balance, AccountBalance)
            .With(a => a.Status, AccountStatus.Live)
            .Create();

        _request = fixture.Build<MakePaymentRequest>()
            .With(r => r.DebtorAccountNumber, ValidAccountNumber)
            .With(r => r.Amount, RequestAmount)
            .Create();

        _accountRepositoryMock = new Mock<IAccountRepository>();
        _accountRepositoryMock.Setup(r => r.GetAccount(ValidAccountNumber)).Returns(_account);

        var accountRepositoryFactoryMock = new Mock<IAccountRepositoryFactory>();
        accountRepositoryFactoryMock.Setup(f => f.GetAccountRepository()).Returns(_accountRepositoryMock.Object);

        _sut = new PaymentService(new PaymentTransferService(), accountRepositoryFactoryMock.Object);
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs, PaymentScheme.Bacs)]
    [InlineData(AllowedPaymentSchemes.FasterPayments, PaymentScheme.FasterPayments)]
    [InlineData(AllowedPaymentSchemes.Chaps, PaymentScheme.Chaps)]
    public void MakePayment_AllowedPaymentScheme_ReturnsSuccess(AllowedPaymentSchemes allowedSchemes, PaymentScheme requestScheme)
    {
        // Arrange
        _account.AllowedPaymentSchemes = allowedSchemes;

        _request.PaymentScheme = requestScheme;

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.True(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == AccountBalance - RequestAmount)), Times.Once);
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs, PaymentScheme.FasterPayments)]
    [InlineData(AllowedPaymentSchemes.Bacs, PaymentScheme.Chaps)]
    [InlineData(AllowedPaymentSchemes.FasterPayments, PaymentScheme.Bacs)]
    [InlineData(AllowedPaymentSchemes.FasterPayments, PaymentScheme.Chaps)]
    [InlineData(AllowedPaymentSchemes.Chaps, PaymentScheme.Bacs)]
    [InlineData(AllowedPaymentSchemes.Chaps, PaymentScheme.FasterPayments)]
    public void MakePayment_UnallowedPaymentScheme_ReturnsFailure(AllowedPaymentSchemes allowedSchemes, PaymentScheme requestScheme)
    {
        // Arrange
        _account.AllowedPaymentSchemes = allowedSchemes;

        _request.PaymentScheme = requestScheme;

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.False(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }

    [Theory]
    [InlineData(PaymentScheme.Bacs)]
    [InlineData(PaymentScheme.FasterPayments)]
    [InlineData(PaymentScheme.Chaps)]
    public void MakePayment_NonExistingAccount_ReturnsFailure(PaymentScheme scheme)
    {
        // Arrange
        _request.DebtorAccountNumber = NonExistingAccountNumber;
        _request.PaymentScheme = scheme;

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.False(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }

    [Theory]
    [InlineData(AccountStatus.Disabled)]
    [InlineData(AccountStatus.InboundPaymentsOnly)]
    public void MakePayment_ChapsWithNonLiveStatus_ReturnsFailure(AccountStatus status)
    {
        // Arrange
        _account.AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps;
        _account.Status = status;

        _request.PaymentScheme = PaymentScheme.Chaps;

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.False(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }

    [Theory]
    [InlineData(AllowedPaymentSchemes.Bacs, PaymentScheme.Bacs)]
    [InlineData(AllowedPaymentSchemes.FasterPayments, PaymentScheme.FasterPayments)]
    [InlineData(AllowedPaymentSchemes.Chaps, PaymentScheme.Chaps)]
    public void MakePayment_ChapsWithLiveStatus_ReturnsSuccess(AllowedPaymentSchemes allowedSchemes, PaymentScheme requestScheme)
    {
        // Arrange
        _account.AllowedPaymentSchemes = allowedSchemes;
        _account.Status = AccountStatus.Live;

        _request.PaymentScheme = requestScheme;

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.True(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == AccountBalance - RequestAmount)), Times.Once);
    }

    [Fact]
    public void MakePayment_FasterPaymentsWithInsufficientBalance_ReturnsFailure()
    {
        // Arrange
        _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
        _account.Balance = 10;

        _request.PaymentScheme = PaymentScheme.FasterPayments;
        _request.Amount = 30m; // Set amount greater than balance

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.False(result.Success);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Never);
    }

    [Fact]
    public void MakePayment_FasterPaymentsWithSufficientBalance_ReturnsSuccess()
    {
        // Arrange
        _account.AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments;
        _account.Balance = 50m;
        _account.Status = AccountStatus.Live;

        _request.PaymentScheme = PaymentScheme.FasterPayments;
        _request.Amount = 20m; // Set amount less than balance

        // Act
        var result = _sut.MakePayment(_request);

        // Assert
        Assert.True(result.Success);

        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.IsAny<Account>()), Times.Once);
        _accountRepositoryMock.Verify(r => r.UpdateAccount(It.Is<Account>(a => a.Balance == 50m - 20m)), Times.Once);
    }
}
