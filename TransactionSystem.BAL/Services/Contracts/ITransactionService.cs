using TransactionSystem.DAL.Entities;

namespace TransactionSystem.BAL.Services.Contracts
{
    public interface ITransactionService
    {
        Task<string> CreateAsync(string name, decimal initialBalance, string accountNumber);
        Task<string> DepositAsync(string accountNumber, decimal amount);
        Task<string> WithdrawAsync(string accountNumber, decimal amount);
        Task<string> CheckBalanceAsync(string accountNumber);
        Task<string> TransferAsync(string sourceAccountNumber, string destinationAccountNumber, decimal amount);
        Task<string> DeleteAsync(string accountNumber);
        Task<string> EditAsync(string accountNumber, string name);
        Task<List<Account>> GetAllAsync();

    }
}
