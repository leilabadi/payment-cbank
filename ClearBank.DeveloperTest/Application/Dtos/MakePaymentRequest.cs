using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Application.Types;

public class MakePaymentRequest
{
    public required string CreditorAccountNumber { get; set; }

    public required string DebtorAccountNumber { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public PaymentScheme PaymentScheme { get; set; }
}
