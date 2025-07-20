namespace ClearBank.DeveloperTest.Domain.Model.Enums;

[Flags]
public enum AllowedPaymentSchemes
{
    FasterPayments = 1 << 0,
    Bacs = 1 << 1,
    Chaps = 1 << 2
}
