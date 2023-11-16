using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace Promotion3x1Fidelity
{
    public class Promotion3x1FidelityLogic : IPromotionable
    {
        private const int _minQuantity = 3;
        private const int _numberOfProductsToTake = 2;
        public string Name { get; } = "3x1 Fidelity";

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
            return productsForPromotion.GroupBy(product => product.Brand.Name)
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
            if (!IsApplicable(cart))
            {
                throw new LogicException("Not applicable promotion");
            }
            decimal currentDiscount = 0;
            foreach (var group in productsForPromotion.GroupBy(product => product.Brand.Name))
            {
                if (group.Count() >= _minQuantity)
                {
                    var cheapestProducts = group.OrderBy(product => product.Price)
                                                .Take(_numberOfProductsToTake);

                    currentDiscount += cheapestProducts.Sum(product => product.Price);
                }
            }
            return (int)Math.Round(currentDiscount);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}