using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Validators;

namespace ClearBank.DeveloperTest.Domain.Model;

public class BacsTransaction : TransferTransaction
{
    public BacsTransaction(decimal amount, DateTime transactionDate, Account sourceAccount, Account destinationAccount)
        : base(amount, transactionDate, new BacsTransactionValidator(), sourceAccount, destinationAccount, PaymentScheme.Bacs)
    {
    }
}
