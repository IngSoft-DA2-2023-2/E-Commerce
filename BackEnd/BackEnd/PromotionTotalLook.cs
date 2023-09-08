using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class PromotionTotalLook : IPromotionable
    {
        private const int MinimumSameColorProducts = 3;
        private const float DiscountPercentage = 0.5f;

        public bool IsApplicable(Purchase purchase)
        {
            List<string> colorsInCart = GetDistinctColorsInCart(purchase.Cart);

            return colorsInCart.Any(color => GetProductsOfColor(purchase.Cart, color).Count >= MinimumSameColorProducts);
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase))
            {
                throw new BackEndException("Not applicable promotion");
            }

            List<string> colorsInCart = GetDistinctColorsInCart(purchase.Cart);

            int maxPrice = 0;
            foreach (string color in colorsInCart)
            {
                List<Product> productsOfSpecificColor = GetProductsOfColor(purchase.Cart, color);

                if (productsOfSpecificColor.Count >= MinimumSameColorProducts)
                {
                    int colorMaxPrice = productsOfSpecificColor.Max(product => product.Price);
                    maxPrice = Math.Max(maxPrice, colorMaxPrice);
                }
            }

            return (int)(maxPrice * DiscountPercentage);
        }

        private List<string> GetDistinctColorsInCart(List<Product> products)
        {
            List<string> colorList = new List<string>();

            foreach (Product product in products)
            {
                colorList.AddRange(product.Color.Where(color => !colorList.Contains(color)));
            }

            return colorList.Distinct().ToList();
        }

        private List<Product> GetProductsOfColor(List<Product> cart, string color)
        {
            return cart.Where(product => product.Color.Contains(color)).ToList();
        }
    }
}
