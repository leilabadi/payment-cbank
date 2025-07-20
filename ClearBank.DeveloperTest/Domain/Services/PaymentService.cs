using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Types;

namespace ClearBank.DeveloperTest.Domain.Services;

public class PaymentService
(
    IAccountRepository accountRepository
) : IPaymentService
{
    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        Account account = accountRepository.GetAccount(request.DebtorAccountNumber);

        DateTime now = DateTime.UtcNow;
        PaymentTransaction? transaction = null;

        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                transaction = new BacsTransaction(request.Amount, now, account, null);
                break;

            case PaymentScheme.FasterPayments:
                transaction = new FasterPaymentsTransaction(request.Amount, now, account, null);
                break;

            case PaymentScheme.Chaps:
                transaction = new ChapsTransaction(request.Amount, now, account, null);
                break;
        }

        if (transaction != null && transaction.IsValid())
        {
            account.Balance -= request.Amount;
            accountRepository.UpdateAccount(account);

            return new MakePaymentResult
            {
                Success = true,
            };
        }

        return new MakePaymentResult
        {
            Success = false,
        };
    }
}
