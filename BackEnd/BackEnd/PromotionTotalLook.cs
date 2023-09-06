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

            List<string> colorList = new List<string>();
            foreach (Product p in purchase.Cart)
            {
                foreach(string color in colorList)
                {
                    if (!colorList.Contains(color)) colorList.Add(color);
                }
            }
            foreach(string color in colorList)
            {
                List<Product> l = purchase.Cart.FindAll(c => c.Color.Contains(color));
                if (l.Count == 3) return true;
            }
            return false;
        }
    }
}
