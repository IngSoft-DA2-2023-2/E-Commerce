using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

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
