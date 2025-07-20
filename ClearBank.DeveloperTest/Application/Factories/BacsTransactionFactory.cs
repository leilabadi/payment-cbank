using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Application.Factories;

public class BacsTransactionFactory : ITransferTransactionFactory
{
    public TransferTransaction CreateTransaction(decimal amount, Account sourceAccount, Account destinationAccount)
    {
        return new BacsTransaction(amount, DateTime.UtcNow, sourceAccount, destinationAccount);
    }
}
