using TransactionSystem.BAL.Services.Contracts;
using TransactionSystem.Core.Constants;
using TransactionSystem.Core.Helpers;
using TransactionSystem.DAL.Entities;
using TransactionSystem.DAL.Services.Contracts;

namespace TransactionSystem.BAL.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;

        public TransactionService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
       
        public async Task<string> CreateAsync(string name, decimal initialBalance, string accountNumber)
        {
            var account = await _accountRepository.Get(accountNumber);
            if (account != null)
            {
                throw new Exception(AccountConstants.AccountExistsMessage);
            }

            string validationAmount = AmountValidator.ValidateAmount(initialBalance);

            string validationMessage = NumberValidator.Validator(accountNumber);

            var createdAccount = new Account { Name = name, Balance = initialBalance, AccountNumber = accountNumber };
            if (createdAccount != null)
            {
                await _accountRepository.Add(createdAccount);
            }

            return AccountConstants.AccountCreatedMessage;

        }

        public async Task<string> CheckBalanceAsync(string accountNumber)
        {

            var account = await _accountRepository.Get(accountNumber);
            if (account == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            string balance = $"Account balance for {accountNumber}: {account.Balance}";
            return balance;

        }

        public async Task<string> DepositAsync(string accountNumber, decimal amount)
        {

            var account = await _accountRepository.Get(accountNumber);
            if (account == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            string validationMessage = AmountValidator.ValidateAmount(amount);

            account.Balance += amount;
            string deposit = $"Deposited {amount} into account {accountNumber}. New balance: {account.Balance}";
            return deposit;

        }

        public async Task<string> TransferAsync(string sourceAccountNumber, string destinationAccountNumber, decimal amount)
        {
            var sourceAccount = await _accountRepository.Get(sourceAccountNumber);
            var destinationAccount = await _accountRepository.Get(destinationAccountNumber);
            if (sourceAccount == null || destinationAccount == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            string validationMessage = AmountValidator.ValidateAmount(amount);

            if (sourceAccount.Balance < amount)
            {
                throw new Exception(AccountConstants.InsufficientBalanceMessage);
            }

            sourceAccount.Balance -= amount;
            destinationAccount.Balance += amount;

            //await WithdrawAsync(sourceAccountNumber, amount);
            //await DepositAsync(destinationAccountNumber, amount); 

            string transfer = $"Transferred {amount} from account {sourceAccountNumber} to account {destinationAccountNumber}.";
            return transfer;

        }

        public async Task<string> WithdrawAsync(string accountNumber, decimal amount)
        {

            var account = await _accountRepository.Get(accountNumber);
            if (account == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            string validationMessage = AmountValidator.ValidateAmount(amount);

            if (account.Balance < amount)
            {
                throw new Exception(AccountConstants.InsufficientBalanceMessage);
            }

            account.Balance -= amount;
            string withdraw = $"Withdrawn {amount} from account {accountNumber}. New balance: {account.Balance}";
            return withdraw;

        }

        public async Task<string> DeleteAsync(string accountNumber)
        {
            var account = await _accountRepository.Get(accountNumber);
            if (account == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            await _accountRepository.Delete(accountNumber);
            return AccountConstants.DeletedAccount;
        }

        public async Task<string> EditAsync(string accountNumber, string name)
        {
            var account = await _accountRepository.Get(accountNumber);
            if (account == null)
            {
                throw new Exception(AccountConstants.AccountNotFoundMessage);
            }

            account.Name = name;
            await _accountRepository.Edit(account);

            return AccountConstants.EditedAccount;
        }

        public async Task<List<Account>> GetAllAsync()
        {
            List<Account> allAccounts = await _accountRepository.GetAll();
            if (allAccounts.Count() == 0 )
            {
                throw new Exception(AccountConstants.AllAccountsError);
            }

            return allAccounts.ToList();
        }
    }
}
