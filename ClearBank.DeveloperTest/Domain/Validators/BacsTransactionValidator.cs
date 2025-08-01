using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Domain.Validators;

public class BacsTransactionValidator : ITransactionValidator
{
    public bool IsValid(Account account, PaymentTransaction transaction)
    {
        return account.IsPaymentSchemeAllowed(AllowedPaymentSchemes.Bacs);
    }
}
