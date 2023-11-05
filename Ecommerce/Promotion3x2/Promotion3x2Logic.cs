using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace Promotion3x2
{
    public class Promotion3x2Logic : IPromotionable
    {
        private const int _minQuantity = 3;
        public string Name { get; } = "3x2";

        public bool IsApplicable(List<Product> cart)
        {
            return cart.GroupBy(product => product.Category.Name)
                                  .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart)) throw new LogicException("Not applicable promotion");

            decimal discount = 0;


            foreach (var group in cart.GroupBy(product => product.Category.Name))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProduct = group.OrderBy(product => product.Price)
                                               .First();
                    discount += cheapestProduct.Price;
                }
            }

            return (int)Decimal.Round(discount);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}