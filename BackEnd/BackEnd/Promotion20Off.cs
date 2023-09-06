using System;

namespace BackEnd
{
    public class Promotion20Off
    {
        public bool IsApplicable(Purchase p)
        {
            return p.Cart.Count >= 2;
        }
        public int ApplyDiscount(Purchase p)
        {
            if(IsApplicable(p)) { return (int)(100 * .8 + 50); }
            throw new BackEndException("Not applicable promotion");
        }


    }
}