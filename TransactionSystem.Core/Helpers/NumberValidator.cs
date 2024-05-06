using TransactionSystem.Core.Constants;

namespace TransactionSystem.Core.Helpers
{
    public class NumberValidator
    {
        public static string Validator(string accountNumber)
        {
            string[] forbiddenChars = { "-", "!", "*", " ", "'", "$", "@" };

            bool containsForbiddenChars = forbiddenChars.Any(charSet => accountNumber.Contains(charSet));
            return containsForbiddenChars ? throw new Exception(AccountConstants.NumberValidation) : null; 
        }

    }
}
