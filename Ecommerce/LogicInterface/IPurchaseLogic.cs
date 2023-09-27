using Domain;

namespace LogicInterface
{
    public interface IPurchaseLogic
    {
        public Purchase CreatePurchase(Purchase purchase);
        public bool IsEligibleForPromotions(Purchase purchase);
        public void AssignsBestPromotion(Purchase purchase);
        public IEnumerable<Purchase> GetPurchase();
    }
}
