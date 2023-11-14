using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.Promotions
{
    public class PromotionContext
    {
        private List<IPromotionable> _promotions;

        public PromotionContext()
        {
            _promotions = new List<IPromotionable>();
        }

        public void SetListPromotions(List<IPromotionable> promotions)
        {
            _promotions = promotions;
        }

        public bool IsEligibleForPromotions(List<Product> cart)
        {
            List<Product> validProducts = new List<Product>();
            foreach (Product p in cart)
            {
                if (p.IncludeForPromotion)
                {
                    validProducts.Add(p);
                }
            }
            bool isValid = false;
            foreach (IPromotionable promo in _promotions)
            {
                if (promo.IsApplicable(validProducts)) isValid = true;
            }
            return isValid;
        }

        public string GetBestPromotion(List<Product> cart)
        {
            List<Product> validProducts = new List<Product>();
            foreach (Product p in cart)
            {
                if (p.IncludeForPromotion) validProducts.Add(p);
            }
            if (!IsEligibleForPromotions(validProducts)) throw new LogicException("Not eligible for promotions.");
            string best = "";
            int maxDiscount = 0;
            foreach (IPromotionable promotion in _promotions)
            {
                if (promotion.IsApplicable(validProducts))
                {
                    int currentDiscount = promotion.CalculateDiscount(validProducts);
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