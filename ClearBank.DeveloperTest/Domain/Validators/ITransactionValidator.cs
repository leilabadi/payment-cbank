using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Domain.Validators;

public interface ITransactionValidator
{
    bool IsValid(Account account, PaymentTransaction transaction);
}
