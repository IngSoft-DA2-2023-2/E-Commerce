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
            throw new NotImplementedException();
        }

        public bool IsEligibleForPromotions(Purchase purchase)
        {
          throw new NotImplementedException();
        }
    }
}
