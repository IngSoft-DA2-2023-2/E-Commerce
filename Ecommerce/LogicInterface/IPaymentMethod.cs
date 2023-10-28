using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface
{
    public interface IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName);

    }
}
