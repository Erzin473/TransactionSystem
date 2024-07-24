using TransactionSystem.Core.Enums;

namespace TransactionSystem.BAL.Services.Contracts
{
    public interface IUISystemService
    {
        bool Exit { get; set; }
        void DisplayMenuOptions();
        string GetUserChoice();
        Task ProcessUserOption(MenuOption option);
    }
}
