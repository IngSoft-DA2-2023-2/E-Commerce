using System;

namespace BackEnd
{
    public class Promotion20Off
    {
        private const float _twentyPercent = 0.2f;

        public bool IsApplicable(Purchase p)
        {
            return p.Cart.Count >= 2;
        }

        public int CalculateDiscount(Purchase p)
        {
            if (!IsApplicable(p))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int maxPrice = 0;
            foreach (Product item in p.Cart)
            {
                if (item.Price > maxPrice)
                {
                    maxPrice = item.Price;
                }
            }

            return (int)(_twentyPercent * maxPrice);
        }


    }
}