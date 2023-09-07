using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public interface IPromotionable
    {
        bool IsApplicable(Purchase purchase);

        int CalculateDiscount(Purchase purchase);
    }
}
