using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class PromotionTotalLook
    {
        public bool IsApplicable(Purchase purchase)
        {
            if(purchase.Cart.Count == 3) return true;
            return false;
        }
    }
}
