using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.Promotions
{
    public class PromotionTotalLook : IPromotionable
    {
        private const int MinimumSameColorProducts = 3;
        private const decimal DiscountPercentage = 0.5m;
        public string Name { get; } = "TotalLook";

        public bool IsApplicable(List<Product> cart)
        {
            List<Colour> colorsInCart = GetDistinctColorsInCart(cart);

            return colorsInCart.Any(color => GetProductsOfColor(cart, color).Count >= MinimumSameColorProducts);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart))
            {
                throw new LogicException("Not applicable promotion");
            }

            List<Colour> colorsInCart = GetDistinctColorsInCart(cart);

            decimal maxPrice = 0;
            foreach (Colour color in colorsInCart)
            {
                List<Product> productsOfSpecificColor = GetProductsOfColor(cart, color);

                if (productsOfSpecificColor.Count >= MinimumSameColorProducts)
                {
                    int colorMaxPrice = productsOfSpecificColor.Max(product => product.Price);
                    maxPrice = Math.Max(maxPrice, colorMaxPrice);
                }
            }

            return (int)(maxPrice * DiscountPercentage);
        }

        private static List<Colour> GetDistinctColorsInCart(List<Product> products)
        {
            List<Colour> colorList = new();

            foreach (Product product in products)
            {
                colorList.AddRange(product.Color.Where(color => !colorList.Contains(color)));
            }

            return colorList.Distinct().ToList();
        }

        private static List<Product> GetProductsOfColor(List<Product> cart, Colour color)
        {
            return cart.Where(product => product.Color.Contains(color)).ToList();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
