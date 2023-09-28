using Domain;

namespace DataAccessInterface
{
    public interface IPurchaseRepository
    {
        public Purchase CreatePurchase(Purchase purchase);
        public IEnumerable<Purchase> GetPurchases(Guid? id);
    }
}