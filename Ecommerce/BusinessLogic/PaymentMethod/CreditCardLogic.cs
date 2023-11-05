using LogicInterface;

namespace BusinessLogic.PaymentMethod
{
    public class CreditCardLogic : IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName)
        {
            return total;
        }
    }
}
