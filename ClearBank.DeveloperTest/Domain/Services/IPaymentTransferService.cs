using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Domain.Services;

public interface IPaymentTransferService
{
    PaymentResult Transfer(TransferTransaction transaction);
}
