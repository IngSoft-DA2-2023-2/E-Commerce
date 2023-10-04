using BusinessLogic.Promotions;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
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
            purchase.CurrentPromotion = _promotionContext.GetBestPromotion(purchase.Cart);
            purchase.Total = _promotionContext.CalculateTotalWithPromotion(purchase.Cart);
        }

        public Purchase CreatePurchase(Purchase purchase)
        {
            try{
                Guid guid = Guid.NewGuid();
                purchase.Id = guid;
                purchase.Total = _promotionContext.CalculateTotalWithoutPromotion(purchase.Cart);
                if (IsEligibleForPromotions(purchase)) AssignsBestPromotion(purchase);
                return _purchaseRepository.CreatePurchase(purchase);
            }catch (DataAccessException e)
            {
                throw new LogicException(e);
            }           
        }

        public IEnumerable<Purchase> GetPurchase(Guid id)
        {
            return _purchaseRepository.GetPurchase(id);
        }

        private bool IsEligibleForPromotions(Purchase purchase)
        {
           return _promotionContext.IsEligibleForPromotions(purchase.Cart);   
        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
           return _purchaseRepository.GetAllPurchases();
        }
    }
}
