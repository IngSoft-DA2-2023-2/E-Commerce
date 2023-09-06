using System;

namespace BackEnd
{
    public class Promotion
    {
        public bool IsApplicable(Purchase p)
        {
            if (p.Cart.Count == 2) return true;
            return false;
        }
    }
}