using LogicInterface;

namespace BusinessLogic.PaymentMethod
{
    public class PaganzaLogic : IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName)
        {
            if (categoryName == "Paganza")
            {
                return (int)(total * 0.9);
            }
            return total;
        }
    }
}
