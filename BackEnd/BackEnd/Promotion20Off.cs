using System;

namespace BackEnd
{
    public class Promotion20Off
    {
        public bool IsApplicable(Purchase p)
        {
            return p.Cart.Count >= 2;
        }

    }
}