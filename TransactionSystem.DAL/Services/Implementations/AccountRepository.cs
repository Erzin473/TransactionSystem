using TransactionSystem.Core.Constants;
using TransactionSystem.DAL.Entities;
using TransactionSystem.DAL.Services.Contracts;

namespace TransactionSystem.DAL.Services.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private List<Account> accounts = new List<Account>();
        public async Task Add(Account account)
        {
            accounts.Add(account);
            await Task.CompletedTask;
        }

        public async Task<Account> Get(string accountNumber)
        {
            var account = accounts.FirstOrDefault(acc => acc.AccountNumber == accountNumber);
            await Task.CompletedTask;

            return account;
        }

        public async Task Edit(Account account)
        {
            var existingAccount = await Get(account.AccountNumber);
            if (existingAccount == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            existingAccount.Name = account.Name;
            await Task.CompletedTask;
        }

        public async Task Delete(string accountNumber)
        {
            var accountToDelete = await Get(accountNumber);
            if (accountToDelete == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            accounts.Remove(accountToDelete);
            await Task.CompletedTask;
        }

        public async Task<List<Account>> GetAll()
        {
            await Task.CompletedTask;
            return accounts;
        }
    }
}
