using Moq;
using TransactionSystem.BAL.Services.Implementations;
using TransactionSystem.DAL.Entities;
using TransactionSystem.DAL.Services.Contracts;

namespace TransactionSystem.Tests
{
    public class Tests
    {
        private IAccountRepository _accountRepository;
        private TransactionService _transactionService;

        [SetUp]
        public void Setup()
        {
            var mockAccountRepository = new Mock<IAccountRepository>();

            mockAccountRepository.Setup(repo => repo.Get(It.IsAny<string>()))
                            .Returns((string accountNumber) =>
                            {
                                if (accountNumber == "11")
                                {
                                    return Task.FromResult(new Account { Name = "Jack Smith", Balance = 10000, AccountNumber = "11" });
                                }
                                else if (accountNumber == "33")
                                {
                                    return Task.FromResult(new Account { Name = "Jack Smith", Balance = 10000, AccountNumber = "33" });

                                }
                                else
                                {
                                    return Task.FromResult<Account>(null);
                                }
                            });


            var sourceAccount = new Account { Name = "Will Doe", Balance = 1000, AccountNumber = "2345678" };
            var destinationAccount = new Account { Name = "Jane Smith", Balance = 500, AccountNumber = "23456789" };
            var accountNumber = new Account { Name = "Jane Smith", Balance = 500, AccountNumber = "44" };
            var accountNumberWithdraw = new Account { Name = "Jane Smith", Balance = 10000, AccountNumber = "22" };


            mockAccountRepository.Setup(repo => repo.Get("2345678"))
                            .Returns(Task.FromResult(sourceAccount));

            mockAccountRepository.Setup(repo => repo.Get("23456789"))
                            .Returns(Task.FromResult(destinationAccount));

            mockAccountRepository.Setup(repo => repo.Get("44"))
                            .Returns(Task.FromResult(accountNumber));

            mockAccountRepository.Setup(repo => repo.Get("22"))
                            .Returns(Task.FromResult(accountNumberWithdraw));


            _accountRepository = mockAccountRepository.Object;
            _transactionService = new TransactionService(_accountRepository);
        }

        [Test]
        public async Task TestCreateAccount_Success()
        {
            string accountNumber = "11";

            Assert.That(async () => await _transactionService.CheckBalanceAsync(accountNumber), Throws.Nothing);
        }

        [Test]
        public async Task TestWithdraw_Success()
        {
            string accountNumber = "22";
            decimal initialBalance = 10000;
            decimal withdrawalAmount = 500;

            await _transactionService.WithdrawAsync(accountNumber, withdrawalAmount);

            Account account = await _accountRepository.Get(accountNumber);
            Assert.That(account.Balance, Is.EqualTo(initialBalance - withdrawalAmount));
        }

        [Test]
        public async Task TestCheckBalance_Success()
        {
            string accountNumber = "33";
            decimal initialBalance = 10000;

            Account account = await _accountRepository.Get(accountNumber);
            await _transactionService.CheckBalanceAsync(account.AccountNumber);

            Assert.That(account.Balance, Is.EqualTo(initialBalance));
        }

        [Test]
        public async Task TestDeposit_Success()
        {
            string accountNumber = "44";
            decimal initialBalance = 500;
            decimal depositAmount = 300;

            await _transactionService.DepositAsync(accountNumber, depositAmount);

            // Assert
            Account account = await _accountRepository.Get(accountNumber);
            Assert.That(account.Balance, Is.EqualTo(initialBalance + depositAmount));

        }

        [Test]
        public async Task TestTransfer_Success()
        {
            string sourceAccountNumber = "2345678";
            decimal sourceInitialBalance = 1000;
            
            string destinationAccountNumber = "23456789";
            decimal destinationInitialBalance = 500;

            decimal transferAmount = 300;

            await _transactionService.TransferAsync(sourceAccountNumber, destinationAccountNumber, transferAmount);

            Account sourceAccount = await _accountRepository.Get(sourceAccountNumber);
            Account destinationAccount = await _accountRepository.Get(destinationAccountNumber);

            Assert.That(sourceAccount.Balance, Is.EqualTo(sourceInitialBalance - transferAmount));
            Assert.That(destinationAccount.Balance, Is.EqualTo(destinationInitialBalance + transferAmount));
        }
    }
}