using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.Promotions
{
    public class PromotionContext
    {
        private readonly List<IPromotionable> _promotions;
        public PromotionContext()
        {
            _promotions = new List<IPromotionable>();
        }

        public void AddPromotion(IPromotionable promotion)
        {
            _promotions.Add(promotion);
        }

        public List<IPromotionable> GetPromotions()
        {
           return _promotions;
        }

        public bool IsEligibleForPromotions(List<Product> cart)
        {
            return _promotions.Any(promotion => promotion.IsApplicable(cart));
        }
        public string GetBestPromotion(List<Product> cart)
        {
            if (!IsEligibleForPromotions(cart)) throw new LogicException("Not Eligible for promotions");
            string best = "";
            int maxDiscount = 0;
            foreach (IPromotionable promotion in _promotions)
            {
                if (promotion.IsApplicable(cart))
                {
                    int currentDiscount = promotion.CalculateDiscount(cart);
                    if (currentDiscount > maxDiscount)
                    {
                        best = promotion.ToString();
                        maxDiscount = currentDiscount;
                    }
                }
            }
            return best;
        }

        public int CalculateTotalWithoutPromotion(List<Product> cart)
        {
            int prices = 0;
            foreach (Product product in cart) prices += product.Price;
            return prices;
        }
        public int CalculateTotalWithPromotion(List<Product> cart)
        {
            int prices = 0;
            foreach (Product product in cart) prices += product.Price;

            int maxDiscount = 0;
            foreach (IPromotionable promotion in _promotions)
            {
                if (promotion.IsApplicable(cart))
                {
                    int currentDiscount = promotion.CalculateDiscount(cart);
                    if (currentDiscount > maxDiscount)
                    {
                        maxDiscount = currentDiscount;
                    }
                }
            }
            return prices - maxDiscount;
        }
    }
}
