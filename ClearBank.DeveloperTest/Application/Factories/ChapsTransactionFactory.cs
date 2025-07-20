using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Application.Factories;

public class ChapsTransactionFactory : ITransferTransactionFactory
{
    public TransferTransaction CreateTransaction(decimal amount, Account sourceAccount, Account destinationAccount)
    {
        return new ChapsTransaction(amount, DateTime.UtcNow, sourceAccount, destinationAccount);
    }
}
