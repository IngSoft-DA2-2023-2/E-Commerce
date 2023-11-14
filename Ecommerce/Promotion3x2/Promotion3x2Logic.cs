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
            List<Product> productsForPromotion = new List<Product>();
            foreach (Product product in cart)
            {
                if (product.IncludeForPromotion)
                {
                    productsForPromotion.Add(product);
                }
            }
            return productsForPromotion.GroupBy(product => product.Category.Name)
                                  .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            List<Product> productsForPromotion = new List<Product>();
            foreach (Product product in cart)
            {
                if (product.IncludeForPromotion)
                {
                    productsForPromotion.Add(product);
                }
            }
            if (!IsApplicable(cart)) throw new LogicException("Not applicable promotion");
            decimal discount = 0;
            foreach (var group in productsForPromotion.GroupBy(product => product.Category.Name))
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