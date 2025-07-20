using ClearBank.DeveloperTest.Domain.Model.Enums;
using ClearBank.DeveloperTest.Domain.Validators;

namespace ClearBank.DeveloperTest.Domain.Model;

public abstract class TransferTransaction
(
    decimal amount,
    DateTime transactionDate,
    ITransactionValidator validator,
    Account sourceAccount,
    Account destinationAccount,
    PaymentScheme paymentScheme
) : PaymentTransaction(amount, transactionDate, validator)
{
    public Account SourceAccount { get; } = sourceAccount;
    public Account DestinationAccount { get; } = destinationAccount;
    public PaymentScheme PaymentScheme { get; } = paymentScheme;

    public override bool IsValid()
    {
        if (SourceAccount == null)
        {
            return false;
        }
        return Validator.IsValid(SourceAccount, this);
    }
}
