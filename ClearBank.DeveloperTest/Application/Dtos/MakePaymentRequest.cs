using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Application.Types;

public class MakePaymentRequest
{
    public string CreditorAccountNumber { get; set; }

    public string DebtorAccountNumber { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public PaymentScheme PaymentScheme { get; set; }
}
