using System;

namespace BackEnd
{
    public class Promotion20Off
    {
        public bool IsApplicable(Purchase p)
        {
            return p.Cart.Count >= 2;
        }
        public int CalculateDiscount(Purchase p)
        {
            if(IsApplicable(p)) {

                int maxPrice = 0;
 
               foreach(Product item in p.Cart) {
                    if(item.Price > maxPrice)
                    {
                        maxPrice = item.Price;
                    }
                }

                return (int)(0.2 * maxPrice);


            }
            throw new BackEndException("Not applicable promotion");
        }


    }
}