using ClearBank.DeveloperTest.Domain.Validators;

namespace ClearBank.DeveloperTest.Domain.Model;

public abstract class PaymentTransaction(decimal amount, DateTime transactionDate, ITransactionValidator validator)
{
    public Guid Id { get; } = Guid.NewGuid();

    // In real world applications, we should handle different currencies
    public decimal Amount { get; } = amount;

    public DateTime TransactionDate { get; } = transactionDate;
    protected ITransactionValidator Validator { get; } = validator;

    public abstract bool IsValid();
}
