using System.Globalization;
using TransactionSystem.BAL.Services.Contracts;
using TransactionSystem.Core.Enums;
using TransactionSystem.DAL.Entities;

namespace TransactionSystem.BAL.Services.Implementations
{
    public class UISystemService : IUISystemService
    {
        private readonly ITransactionService _transactionService;
        public UISystemService(ITransactionService transactionService) 
        {
            _transactionService = transactionService;
        }

        public void DisplayMenuOptions()
        {
            Console.WriteLine("1. Create Account");
            Console.WriteLine("2. Deposit Money");
            Console.WriteLine("3. Withdraw Money");
            Console.WriteLine("4. Check Account Balance");
            Console.WriteLine("5. Transfer Money");
            Console.WriteLine("6. Edit Account Name");
            Console.WriteLine("7. Delete Account");
            Console.WriteLine("8. View All Accounts");
            Console.WriteLine("0. Exit");
        }

        public string GetUserChoice()
        {
            Console.Write("Enter your choice: ");
            return Console.ReadLine();
        }

        public async Task CreateAccount()
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();

            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter initial balance: ");
            decimal initialBalance;
            while (!decimal.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out initialBalance))
            {
                Console.WriteLine("Invalid initial balance. Please enter a valid decimal number.");
                Console.Write("Enter initial balance: ");
            }

            string accountMessage = await _transactionService.CreateAsync(name, initialBalance, accountNumber);
            Console.WriteLine(accountMessage);
        }

        public async Task DepositMoney()
        {
            Console.Write("Enter account number: ");
            string depositAccountNumber = Console.ReadLine();

            Console.Write("Enter deposit amount: ");
            decimal depositAmount;
            while (!decimal.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out depositAmount))
            {
                Console.WriteLine("Invalid deposit amount. Please enter a valid decimal number.");
                Console.Write("Enter deposit amount: ");
            }

            string depositMessage = await _transactionService.DepositAsync(depositAccountNumber, depositAmount);
            Console.WriteLine(depositMessage);
        }

        public async Task WithdrawMoney()
        {
            Console.Write("Enter account number: ");
            string withdrawAccountNumber = Console.ReadLine();

            Console.Write("Enter withdrawal amount: ");
            decimal withdrawAmount;
            while (!decimal.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out withdrawAmount))
            {
                Console.WriteLine("Invalid withdrawal amount. Please enter a valid decimal number.");
                Console.Write("Enter withdrawal amount: ");
            }

            string withdrawMessage = await _transactionService.WithdrawAsync(withdrawAccountNumber, withdrawAmount);
            Console.WriteLine(withdrawMessage);
        }

        public async Task CheckAccountBalance()
        {
            Console.Write("Enter account number: ");
            string checkBalanceAccountNumber = Console.ReadLine();

            string balanceMessage = await _transactionService.CheckBalanceAsync(checkBalanceAccountNumber);
            Console.WriteLine(balanceMessage);
        }

        public async Task TransferMoney()
        {
            Console.Write("Enter source account number: ");
            string sourceAccountNumber = Console.ReadLine();

            Console.Write("Enter destination account number: ");
            string destinationAccountNumber = Console.ReadLine();

            Console.Write("Enter transfer amount: ");
            decimal transferAmount;
            while (!decimal.TryParse(Console.ReadLine().Replace(",", "."), NumberStyles.Any, CultureInfo.InvariantCulture, out transferAmount))
            {
                Console.WriteLine("Invalid transfer amount. Please enter a valid decimal number.");
                Console.Write("Enter transfer amount: ");
            }

            string transferMessage = await _transactionService.TransferAsync(sourceAccountNumber, destinationAccountNumber, transferAmount);
            Console.WriteLine(transferMessage);
        }

        public async Task DeleteAccount()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            string accountMessage = await _transactionService.DeleteAsync(accountNumber);
            Console.WriteLine(accountMessage);
        }

        public async Task EditAccount()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            Console.Write("Enter new name: ");
            string name = Console.ReadLine();

            string accountMessage = await _transactionService.EditAsync(accountNumber, name);
            Console.WriteLine(accountMessage);
        }

        public async Task AllAccount()
        {
            List<Account> allAccounts = await _transactionService.GetAllAsync();
          
            Console.WriteLine("All Accounts:");
            foreach (var account in allAccounts)
            {
                Console.WriteLine($"Account Number: {account.AccountNumber}, Name: {account.Name}, Balance: {account.Balance}");
            }
        }

        public bool Exit { get; set; } 

        public async Task ProcessUserOption(MenuOption option)
        {
            switch (option)
            {
                case MenuOption.CreateAccount:
                    await CreateAccount();
                    break;
                case MenuOption.Deposit:
                    await DepositMoney();
                    break;
                case MenuOption.Withdraw:
                    await WithdrawMoney();
                    break;
                case MenuOption.CheckBalance:
                    await CheckAccountBalance();
                    break;
                case MenuOption.Transfer:
                    await TransferMoney();
                    break;
                case MenuOption.EditAccount:
                    await EditAccount();
                    break;
                case MenuOption.DeleteAccount:
                    await DeleteAccount();
                    break;
                case MenuOption.AllAccount:
                    await AllAccount();
                    break;
                case MenuOption.Exit:
                    Exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}
