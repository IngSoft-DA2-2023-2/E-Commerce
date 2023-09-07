using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class Promotion3x2 : IPromotionable
    {
        private const int _minQuantity = 3;

        public bool IsApplicable(Purchase purchase)
        {
            return purchase.Cart.GroupBy(p => p.Category)
                                 .Any(g => g.Count() >= _minQuantity);
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase)) throw new BackEndException("Not applicable promotion");

            var discount = 0;

            foreach (var group in purchase.Cart.GroupBy(p => p.Category))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProduct = group.OrderBy(p => p.Price)
                                               .First();
                    discount += cheapestProduct.Price;
                }
            }

            return discount;
        }
    }
}