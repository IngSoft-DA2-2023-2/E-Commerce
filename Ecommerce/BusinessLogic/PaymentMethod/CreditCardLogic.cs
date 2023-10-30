using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
