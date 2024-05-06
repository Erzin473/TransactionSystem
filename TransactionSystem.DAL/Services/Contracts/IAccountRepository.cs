using TransactionSystem.DAL.Entities;

namespace TransactionSystem.DAL.Services.Contracts
{
    public interface IAccountRepository
    {
        Task Add(Account account);
        Task<Account> Get(string accountNumber);
        Task Edit(Account account);
        Task Delete(string accountNumber);
        Task<List<Account>> GetAll(); 

    }
}
