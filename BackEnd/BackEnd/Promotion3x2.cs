using System.Linq;

namespace BackEnd
{
    public class Promotion3x2 : IPromotionable
    {
        private const int _minQuantity = 3;

        public bool IsApplicable(Purchase purchase)
        {
            return purchase.Cart.GroupBy(product => product.Category)
                                 .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase)) throw new BackEndException("Not applicable promotion");

            var discount = 0;

            foreach (var group in purchase.Cart.GroupBy(product => product.Category))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProduct = group.OrderBy(product => product.Price)
                                               .First();
                    discount += cheapestProduct.Price;
                }
            }

            return discount;
        }
    }
}