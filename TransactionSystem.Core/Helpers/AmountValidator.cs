using TransactionSystem.Core.Constants;

namespace TransactionSystem.Core.Helpers
{
    public class AmountValidator
    {
        public static string ValidateAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new Exception(AccountConstants.AmountError);
            }

            if (Math.Abs(amount) > 999999999)
            {
                throw new Exception(AccountConstants.InvalidAmount);
            }

            return null;
        }
    }
   
}
