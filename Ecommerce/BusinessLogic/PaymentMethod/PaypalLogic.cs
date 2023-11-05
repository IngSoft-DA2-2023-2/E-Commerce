using LogicInterface;

namespace BusinessLogic.PaymentMethod
{
    public class PaypalLogic : IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName)
        {
            return total;
        }
    }
}
