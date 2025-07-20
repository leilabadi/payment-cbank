using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Validators;

namespace ClearBank.DeveloperTest.Domain.Model;

public class ChapsTransaction : TransferTransaction
{
    public ChapsTransaction(Decimal amount, DateTime transactionDate, Account sourceAccount, Account destinationAccount)
        : base(amount, transactionDate, new ChapsTransactionValidator(), sourceAccount, destinationAccount, PaymentScheme.Chaps)
    {
    }
}
