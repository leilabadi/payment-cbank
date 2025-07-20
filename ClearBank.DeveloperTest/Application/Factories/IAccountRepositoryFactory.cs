using ClearBank.DeveloperTest.Domain.Repositories;

namespace ClearBank.DeveloperTest.Application.Factories;

public interface IAccountRepositoryFactory
{
    public IAccountRepository GetAccountRepository();
}
