using System.Linq;

namespace BackEnd
{
    public class Promotion3x1Fidelity : IPromotionable
    {
        private const int _minQuantity = 3;
        private const int _numberOfProductsToTake = 2;

        public bool IsApplicable(Purchase purchase)
        {
            return purchase.Cart.GroupBy(product => product.Brand)
                                 .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int currentDiscount = 0;

            foreach (var group in purchase.Cart.GroupBy(product => product.Brand))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProducts = group.OrderBy(product => product.Price)
                                                .Take(_numberOfProductsToTake);

                    currentDiscount += cheapestProducts.Sum(product => product.Price);
                }
            }

            return currentDiscount;
        }
    }
}