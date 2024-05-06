using TransactionSystem.Core.Enums;

namespace TransactionSystem.BAL.Services.Contracts
{
    public interface IUISystemService
    {
        bool Exit { get; set; }
        void DisplayMenuOptions();
        string GetUserChoice();
        Task CreateAccount();
        Task DepositMoney();
        Task WithdrawMoney();
        Task CheckAccountBalance();
        Task TransferMoney();
        Task DeleteAccount();
        Task EditAccount();
        Task AllAccount();
        Task ProcessUserOption(MenuOption option);
    }
}
