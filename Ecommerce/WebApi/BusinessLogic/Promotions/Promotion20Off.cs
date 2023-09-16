using System.Collections.Generic;
using WebApi.Domain;
using WebApi.LogicInterface;
using WebApi.ExceptionBackEnd;

namespace WebApi.BusinessLogic.Promotions
{
    public class Promotion20Off : IPromotionable
    {
        private const float _twentyPercent = 0.2f;
        private const int _minCartSize = 2;

        public bool IsApplicable(List<Product> cart)
        {
            return cart.Count >= _minCartSize;
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int maxPrice = 0;
            foreach (Product item in cart)
            {
                if (item.Price > maxPrice)
                {
                    maxPrice = item.Price;
                }
            }

            return (int)(_twentyPercent * maxPrice);
        }


    }
}