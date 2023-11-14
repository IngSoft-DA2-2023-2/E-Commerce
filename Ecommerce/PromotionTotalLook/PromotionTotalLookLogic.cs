using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace PromotionTotalLook
{
    public class PromotionTotalLookLogic : IPromotionable
    {
        private const int MinimumSameColourProducts = 3;
        private const decimal DiscountPercentage = 0.5m;
        public string Name { get; } = "TotalLook";

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
            List<Colour> coloursInCart = GetDistinctColoursInCart(productsForPromotion);

            return coloursInCart.Any(colour => GetProductsOfColour(cart, colour).Count >= MinimumSameColourProducts);
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

            List<Colour> coloursInCart = GetDistinctColoursInCart(productsForPromotion);

            decimal maxPrice = 0;
            foreach (Colour colour in coloursInCart)
            {
                List<Product> productsOfSpecificColour = GetProductsOfColour(cart, colour);

                if (productsOfSpecificColour.Count >= MinimumSameColourProducts)
                {
                    int colourMaxPrice = productsOfSpecificColour.Max(product => product.Price);
                    maxPrice = Math.Max(maxPrice, colourMaxPrice);
                }
            }

            return (int)(maxPrice * DiscountPercentage);
        }

        private static List<Colour> GetDistinctColoursInCart(List<Product> products)
        {
            List<Colour> colourList = new();

            foreach (Product product in products)
            {
                colourList.AddRange(product.Colours.Where(colour => !colourList.Contains(colour)));
            }

            return colourList.Distinct().ToList();
        }

        private static List<Product> GetProductsOfColour(List<Product> cart, Colour colour)
        {
            return cart.Where(product => product.Colours.Contains(colour)).ToList();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}