using ClearBank.DeveloperTest.Domain.Model;
using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Domain.Validators;

public class FasterPaymentsTransactionValidator : ITransactionValidator
{
    public bool IsValid(Account account, PaymentTransaction transaction)
    {
        if (!account.IsPaymentSchemeAllowed(AllowedPaymentSchemes.FasterPayments))
        {
            return false;
        }
        else if (!account.HasSufficientFunds(transaction.Amount))
        {
            return false;
        }
        return true;
    }
}
