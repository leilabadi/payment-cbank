using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Application.Factories;

public interface ITransferTransactionFactory
{
    TransferTransaction CreateTransaction(decimal amount, Account sourceAccount, Account destinationAccount);
}
