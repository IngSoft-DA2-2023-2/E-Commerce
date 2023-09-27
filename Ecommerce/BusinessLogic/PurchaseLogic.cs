using BusinessLogic.Promotions;
using DataAccessInterface;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly PromotionContext _promotionContext;

        public PurchaseLogic(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
            _promotionContext = new PromotionContext();
        }

        private void AssignsBestPromotion(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Purchase CreatePurchase(Purchase purchase)
        {
            Guid guid = Guid.NewGuid();
            purchase.Id = guid;
            if (IsEligibleForPromotions(purchase)) AssignsBestPromotion(purchase);
            return _purchaseRepository.CreatePurchase(purchase);
        }

        public IEnumerable<Purchase> GetPurchases(Guid? id)
        {
            return _purchaseRepository.GetPurchases(id);
        }

        private bool IsEligibleForPromotions(Purchase purchase)
        {
           return _promotionContext.IsEligibleForPromotions(purchase.Cart);   
        }
    }
}
