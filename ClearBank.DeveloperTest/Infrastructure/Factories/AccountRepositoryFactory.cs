using ClearBank.DeveloperTest.Application.Factories;
using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Infrastructure.Repositories;
using System.Configuration;

namespace ClearBank.DeveloperTest.Infrastructure.Factories;

public class AccountRepositoryFactory() : IAccountRepositoryFactory
{
    public IAccountRepository GetAccountRepository()
    {
        var dataStoreType = ConfigurationManager.AppSettings["DataStoreType"];

        if (string.IsNullOrWhiteSpace(dataStoreType))
        {
            throw new Exception("DataStoreType is not configured in app settings.");
        }

        return dataStoreType switch
        {
            "Backup" => new BackupAccountRepository(),
            _ => new AccountRepository(),
        };
    }
}
