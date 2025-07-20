using ClearBank.DeveloperTest.Domain.Model;

namespace ClearBank.DeveloperTest.Domain.Repositories;

public interface IAccountRepository
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}
