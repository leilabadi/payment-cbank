using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Application.Factories;

public class FasterPaymentsTransactionFactory : ITransferTransactionFactory
{
    public TransferTransaction CreateTransaction(decimal amount, Account sourceAccount, Account destinationAccount)
    {
        return new FasterPaymentsTransaction(amount, DateTime.UtcNow, sourceAccount, destinationAccount);
    }
}
