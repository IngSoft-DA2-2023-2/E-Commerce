using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using LogicInterface;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Promotions
{
    public class PromotionTotalLook : IPromotionable
    {
        private const int MinimumSameColorProducts = 3;
        private const float DiscountPercentage = 0.5f;

        public bool IsApplicable(List<Product> cart)
        {
            List<string> colorsInCart = GetDistinctColorsInCart(cart);

            return colorsInCart.Any(color => GetProductsOfColor(cart, color).Count >= MinimumSameColorProducts);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart))
            {
                throw new BusinessLogicException("Not applicable promotion");
            }

            List<string> colorsInCart = GetDistinctColorsInCart(cart);

            int maxPrice = 0;
            foreach (string color in colorsInCart)
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

        private static List<string> GetDistinctColorsInCart(List<Product> products)
        {
            List<string> colorList = new();

            foreach (Product product in products)
            {
                colorList.AddRange(product.Color.Where(color => !colorList.Contains(color)));
            }

            return colorList.Distinct().ToList();
        }

        private static List<Product> GetProductsOfColor(List<Product> cart, string color)
        {
            return cart.Where(product => product.Color.Contains(color)).ToList();
        }
    }
}
