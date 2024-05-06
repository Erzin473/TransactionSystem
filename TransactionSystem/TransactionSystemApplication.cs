using TransactionSystem.BAL.Services.Contracts;
using TransactionSystem.Core.Enums;

namespace TransactionSystem.PL
{
    public class TransactionSystemApplication
    {
        private readonly IUISystemService _uiSystemService;

        public TransactionSystemApplication(IUISystemService uiSystemService)
        {
            _uiSystemService = uiSystemService;
        }

        public async Task RunAsync()
        {
            Console.WriteLine("Welcome to the Transaction System!");
            _uiSystemService.DisplayMenuOptions();

            while (!_uiSystemService.Exit)
            {
                try
                {
                    string choice = _uiSystemService.GetUserChoice();

                    bool isParsed = Enum.TryParse(choice, out MenuOption option);

                    await _uiSystemService.ProcessUserOption(isParsed ? option : MenuOption.InvalidChoice);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

        }

    }
}
