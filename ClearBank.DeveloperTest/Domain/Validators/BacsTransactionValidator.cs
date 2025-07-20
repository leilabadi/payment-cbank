using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Domain.Validators;

public class BacsTransactionValidator : ITransactionValidator
{
    public bool IsValid(Account account, PaymentTransaction transaction)
    {
        if (!account.IsPaymentSchemeAllowed(AllowedPaymentSchemes.Bacs))
        {
            return false;
        }
        return true;
    }
}
