using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.PurchaseLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private readonly IPurchaseLogic _purchaseLogic;

        public PurchaseLogic(IPurchaseLogic promotionLogic)
        {
            _purchaseLogic = promotionLogic;
        }

        public void AssignsBestPromotion(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Purchase CreatePurchase(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public bool IsEligibleForPromotions(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
