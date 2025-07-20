using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Application.Types;
using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Services;

namespace ClearBank.DeveloperTest.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentTransferService _paymentTransferService;
    private readonly IAccountRepository _accountRepository;

    public PaymentService(IPaymentTransferService paymentTransferService, IAccountRepositoryFactory accountRepositoryFactory)
    {
        _paymentTransferService = paymentTransferService;
        _accountRepository = accountRepositoryFactory.GetAccountRepository();
    }

    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        Account sourceAccount = _accountRepository.GetAccount(request.DebtorAccountNumber);

        var factoryResolver = new TransferTransactionFactoryResolver();

        var transferTransactionFactory = factoryResolver.ResolveTransactionFactory(request.PaymentScheme);

        TransferTransaction transferTransaction = transferTransactionFactory.CreateTransaction(request.Amount, sourceAccount, null);

        var result = _paymentTransferService.Transfer(transferTransaction);

        if (result.Success)
        {
            _accountRepository.UpdateAccount(sourceAccount);
            return MakePaymentResult.SuccessResult();
        }

        return MakePaymentResult.FailureResult();
    }
}
