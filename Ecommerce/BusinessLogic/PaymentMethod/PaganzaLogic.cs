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
        public int CalculateDiscount(List<Product> cart)
        {
            throw new NotImplementedException();
        }
    }
}
