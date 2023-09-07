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
        private const float _fiftyPercent = 0.5f;

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

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase))
            {
                throw new BackEndException("Not applicable promotion");
            }
            
            List<string> colorList = ColorsInCart(purchase.Cart);

            int maxPrice = 0;
            foreach (string color in colorList)
            {  
                List<Product> l = ProductsOfSpecificColor(purchase.Cart, color);
                if (l.Count >= _numberOfSameColor)
                {
                    foreach(Product p in l)
                    {
                        if(p.Price > maxPrice) maxPrice = p.Price;
                    }
                }
            }
            return (int)(maxPrice * _fiftyPercent);

        }
    }
}
