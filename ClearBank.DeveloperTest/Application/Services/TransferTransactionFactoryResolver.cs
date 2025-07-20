using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Domain.Model.Enums;

namespace ClearBank.DeveloperTest.Application.Services;

public class TransferTransactionFactoryResolver
{
    private readonly Dictionary<PaymentScheme, ITransferTransactionFactory> _factoryMap = [];

    public TransferTransactionFactoryResolver()
    {
        _factoryMap[PaymentScheme.Bacs] = new BacsTransactionFactory();
        _factoryMap[PaymentScheme.Chaps] = new ChapsTransactionFactory();
        _factoryMap[PaymentScheme.FasterPayments] = new FasterPaymentsTransactionFactory();
    }

    public ITransferTransactionFactory ResolveTransactionFactory(PaymentScheme paymentScheme)
    {
        return _factoryMap[paymentScheme] ?? throw new NotSupportedException($"Payment scheme {paymentScheme} is not supported.");
    }
}