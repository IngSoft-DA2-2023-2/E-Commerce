using Domain;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
