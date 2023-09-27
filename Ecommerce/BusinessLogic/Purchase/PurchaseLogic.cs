using DataAccessInterface;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic.PurchaseLogic
{
    public class PurchaseLogic : IPurchaseLogic
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseLogic(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        private void AssignsBestPromotion(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public Purchase CreatePurchase(Purchase purchase)
        {
            Guid guid = Guid.NewGuid();
            purchase.Id = guid;
            return _purchaseRepository.CreatePurchase(purchase);
        }

        public IEnumerable<Purchase> GetPurchase()
        {
            throw new NotImplementedException();
        }

        private bool IsEligibleForPromotions(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
