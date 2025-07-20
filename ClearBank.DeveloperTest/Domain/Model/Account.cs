using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Domain.Model;

public class Account
{
    public string AccountNumber { get; init; } = null!;

    // In real world applications, we should handle different currencies
    public decimal Balance { get; set; }

    public AccountStatus Status { get; set; }
    public AllowedPaymentSchemes AllowedPaymentSchemes { get; set; }

    public bool IsPaymentSchemeAllowed(AllowedPaymentSchemes scheme)
    {
        return AllowedPaymentSchemes.HasFlag(scheme);
    }

    public bool HasSufficientFunds(decimal amount)
    {
        return Balance >= amount;
    }

    public bool IsAccountLive()
    {
        return Status == AccountStatus.Live;
    }
}
