using Domain;

namespace DataAccessInterface
{
    public interface IPurchaseRepository
    {
        public Purchase CreatePurchase(Purchase purchase);
        public IEnumerable<Purchase> GetPurchase(Guid id);

        public IEnumerable<Purchase> GetAllPurchases();
    }
}