﻿using System.Collections.Generic;
using System.Linq;
using Domain;
using LogicInterface;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Promotions
{
    public class Promotion3x2 : IPromotionable
    {
        private const int _minQuantity = 3;

        public bool IsApplicable(List<Product> cart)
        {
            return cart.GroupBy(product => product.Category)
                                 .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart)) throw new BusinessLogicException("Not applicable promotion");

            var discount = 0;

            foreach (var group in cart.GroupBy(product => product.Category))
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