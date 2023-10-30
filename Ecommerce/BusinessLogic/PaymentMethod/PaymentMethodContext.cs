using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.PaymentMethod
{
    public class PaymentMethodContext
    {
        private List<IPaymentMethod> _paymentMethods;
        public PaymentMethodContext() { 
            _paymentMethods = new List<IPaymentMethod>();
            IPaymentMethod paymentMethod = new PaganzaLogic();
            _paymentMethods.Add(paymentMethod);
        }

        public int CalculateDiscount(int total, string categoryName)
        {
            int newTotal = total;
            foreach (IPaymentMethod paymentMethod in _paymentMethods)
            {
                newTotal = paymentMethod.CalculateDiscount(total, categoryName);
                if (newTotal != total)
                {
                    return newTotal;
                }
            }
            return total;
        }
    }
}
