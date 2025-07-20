using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Validators;

namespace ClearBank.DeveloperTest.Domain.Model;

public class FasterPaymentsTransaction : TransferTransaction
{
    public FasterPaymentsTransaction(Decimal amount, DateTime transactionDate, Account sourceAccount, Account destinationAccount)
        : base(amount, transactionDate, new FasterPaymentsTransactionValidator(), sourceAccount, destinationAccount, PaymentScheme.FasterPayments)
    {
    }
}
