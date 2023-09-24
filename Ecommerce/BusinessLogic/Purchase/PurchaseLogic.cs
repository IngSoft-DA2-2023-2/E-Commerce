using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.PurchaseLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private readonly IPromotionLogic promotionLogic;

        public PurchaseLogic(IPromotionLogic promotionLogic)
        {
            this.promotionLogic = promotionLogic;
        }
        public void AssignsBestPromotion(Purchase purchase)
        {
            if (!IsEligibleForPromotions(purchase)) throw new LogicException("Not eligible for promotions");
            Promotion best = null;
            int maxDiscount = 0;
            foreach (var promo in purchase.Promotions)
            {
                var currentPromo = promotionLogic.GetPromotionable(promo);
                if (currentPromo.IsApplicable(purchase.Cart))
                {
                    int currentDiscount = currentPromo.CalculateDiscount(purchase.Cart);
                    if (currentDiscount > maxDiscount)
                    {
                        best = promo;
                        maxDiscount = currentDiscount;
                    }
                }
            }

            purchase.CurrentPromotion = best;
        }

        public bool IsEligibleForPromotions(Purchase purchase)
        {
            return purchase.Promotions.Any(promo =>
            {
                var promoLogic = promotionLogic.GetPromotionable(promo);
                return promoLogic.IsApplicable(purchase.Cart);

            });
        }
    }
}
