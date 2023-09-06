using System;

namespace BackEnd
{
    public class Promotion20Off
    {
        public bool IsApplicable(Purchase p)
        {
            return p.Cart.Count >= 2;
        }
        public void ApplyDiscount(Purchase purchaseSample)
        {
            throw new BackEndException("Not applicable promotion");
        }


    }
}