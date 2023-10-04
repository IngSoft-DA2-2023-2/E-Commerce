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
            IPromotionable percentageOff = new Promotion20Off();
            IPromotionable fidelity = new Promotion3x1Fidelity();
            IPromotionable getOneFree = new Promotion3x2();
            IPromotionable totalLook = new PromotionTotalLook();
            _promotions.Add(percentageOff);
            _promotions.Add(fidelity);
            _promotions.Add(getOneFree);
            _promotions.Add(totalLook);
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
                if(promotion.IsApplicable(cart))
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
            return prices-maxDiscount;
        }
    }
}
