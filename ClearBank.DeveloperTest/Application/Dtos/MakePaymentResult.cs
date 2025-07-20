namespace ClearBank.DeveloperTest.Application.Types;

public class MakePaymentResult
{
    public bool Success { get; set; }

    private MakePaymentResult()
    {
    }

    public static MakePaymentResult SuccessResult()
    {
        return new MakePaymentResult { Success = true };
    }

    public static MakePaymentResult FailureResult()
    {
        return new MakePaymentResult { Success = false };
    }
}
