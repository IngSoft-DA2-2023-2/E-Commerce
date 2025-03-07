﻿using System.Collections.Generic;
using System.Linq;
using BackEnd.Domain;
using BackEnd.LogicInterface;
using BackEnd.ExceptionBackEnd;

namespace BackEnd.BusinessLogic.Promotions
{
    public class Promotion3x1Fidelity : IPromotionable
    {
        private const int _minQuantity = 3;
        private const int _numberOfProductsToTake = 2;

        public bool IsApplicable(List<Product> cart)
        {
            return cart.GroupBy(product => product.Brand)
                                 .Any(group => group.Count() >= _minQuantity);
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int currentDiscount = 0;

            foreach (var group in cart.GroupBy(product => product.Brand))
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