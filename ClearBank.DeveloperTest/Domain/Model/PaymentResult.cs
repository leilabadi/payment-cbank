namespace ClearBank.DeveloperTest.Domain.Model;

public record PaymentResult
{
    public bool Success { get; private init; }
    public string? ErrorMessage { get; private init; }

    private PaymentResult()
    {
    }

    public static PaymentResult SuccessResult()
    {
        return new PaymentResult { Success = true };
    }

    public static PaymentResult FailureResult(string? errorMessage = null)
    {
        return new PaymentResult { Success = false, ErrorMessage = errorMessage };
    }
}