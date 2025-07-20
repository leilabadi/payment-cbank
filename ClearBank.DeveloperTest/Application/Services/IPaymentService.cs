using ClearBank.DeveloperTest.Application.Types;

namespace ClearBank.DeveloperTest.Application.Services;

public interface IPaymentService
{
    MakePaymentResult MakePayment(MakePaymentRequest request);
}
