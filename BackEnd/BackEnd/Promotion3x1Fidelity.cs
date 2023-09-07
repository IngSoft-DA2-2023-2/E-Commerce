using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class Promotion3x1Fidelity : IPromotionable
    {
        private const int _minQuantity = 3;

        public bool IsApplicable(Purchase purchase)
        {
            return purchase.Cart.GroupBy(p => p.Brand)
                                 .Any(g => g.Count() >= _minQuantity);
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int currentDiscount = 0;

            foreach (var group in purchase.Cart.GroupBy(p => p.Brand))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProducts = group.OrderBy(p => p.Price)
                                                .Take(2);

                    currentDiscount += cheapestProducts.Sum(p => p.Price);
                }
            }

            return currentDiscount;
        }
    }
}