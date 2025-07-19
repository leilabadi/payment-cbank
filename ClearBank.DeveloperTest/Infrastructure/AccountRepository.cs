using ClearBank.DeveloperTest.Domain.Repositories;
using ClearBank.DeveloperTest.Domain.Types;

namespace ClearBank.DeveloperTest.Data
{
    public class AccountRepository : IAccountRepository
    {
        public Account GetAccount(string accountNumber)
        {
            // Access database to retrieve account, code removed for brevity 
            return new Account();
        }

        public void UpdateAccount(Account account)
        {
            // Update account in database, code removed for brevity
        }
    }
}
