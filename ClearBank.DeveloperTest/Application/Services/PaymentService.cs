using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Application.Types;
using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Services;

namespace ClearBank.DeveloperTest.Application.Services;

public class PaymentService
: IPaymentService
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
        Account account = _accountRepository.GetAccount(request.DebtorAccountNumber);
        DateTime now = DateTime.UtcNow;

        TransferTransaction transferTransaction = request.PaymentScheme switch
        {
            PaymentScheme.Bacs => new BacsTransaction(request.Amount, now, account, null),
            PaymentScheme.Chaps => new ChapsTransaction(request.Amount, now, account, null),
            PaymentScheme.FasterPayments => new FasterPaymentsTransaction(request.Amount, now, account, null),
            _ => throw new ArgumentException($"Unsupported payment scheme: {request.PaymentScheme}"),
        };

        var result = _paymentTransferService.Transfer(transferTransaction);

        if (result.Success)
        {
            _accountRepository.UpdateAccount(account);
            return MakePaymentResult.SuccessResult();
        }

        return MakePaymentResult.FailureResult();
    }
}
