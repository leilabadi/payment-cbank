using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Services;
using ClearBank.DeveloperTest.Domain.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Application.Handlers;

public class PaymentHandler
{
    private readonly IPaymentService _paymentService;

    public PaymentHandler()
    {
        var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

        IAccountRepository accountRepository;
        if (dataStoreType == "Backup")
        {
            accountRepository = new BackupAccountRepository();
        }
        else
        {
            accountRepository = new AccountRepository();
        }

        _paymentService = new PaymentService(accountRepository);
    }
    public MakePaymentResult Handle(MakePaymentRequest request)
    {
        return _paymentService.MakePayment(request);
    }
}
