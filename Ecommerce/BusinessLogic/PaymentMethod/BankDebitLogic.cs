using LogicInterface;

namespace BusinessLogic.PaymentMethod
{
    public class BankDebitLogic : IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName)
        {
            return total;
        }
    }
}
