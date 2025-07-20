using ClearBank.DeveloperTest.Domain.Types;

namespace ClearBank.DeveloperTest.Domain.Services;

public interface IPaymentService
{
    MakePaymentResult MakePayment(MakePaymentRequest request);
}
