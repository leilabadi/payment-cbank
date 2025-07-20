using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Domain.Services;

public class PaymentTransferService : IPaymentTransferService
{
    public PaymentResult Transfer(TransferTransaction transferTransaction)
    {
        if (transferTransaction != null && transferTransaction.IsValid())
        {
            var sourceAccount = transferTransaction.SourceAccount;

            sourceAccount.Balance -= transferTransaction.Amount;

            return PaymentResult.SuccessResult();
        }

        return PaymentResult.FailureResult();
    }
}
