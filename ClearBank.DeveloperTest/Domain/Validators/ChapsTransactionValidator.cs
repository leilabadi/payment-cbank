using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Domain.Validators;

public class ChapsTransactionValidator : ITransactionValidator
{
    public bool IsValid(Account account, PaymentTransaction transaction)
    {
        if (!account.IsPaymentSchemeAllowed(AllowedPaymentSchemes.Chaps))
        {
            return false;
        }
        else if (!account.IsAccountLive())
        {
            return false;
        }
        return true;
    }
}
