using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class PromotionTotalLook
    {
        private const int _numberOfSameColor = 3;
        public bool IsApplicable(Purchase purchase)
        {
            List<string> colorList = ColorsInCart(purchase.Cart);
         
            foreach (string color in colorList)
            {
                List<Product> l = ProductsOfSpecificColor(purchase.Cart, color);
                if (l.Count >= _numberOfSameColor) return true;
            }
            return false;
        }

        private List<string> ColorsInCart(List<Product> products)
        {
            List<string> colorList = new List<string>();
            foreach (Product p in products)
            {
                foreach (string color in p.Color)
                {
                    if (!colorList.Contains(color)) colorList.Add(color);
                }
            }
            return colorList;
        }

        public List<Product> ProductsOfSpecificColor(List<Product> cart,string color)
        {
            return cart.FindAll(c => c.Color.Contains(color));
        }
    }
}
