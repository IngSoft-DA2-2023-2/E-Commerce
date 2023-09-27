using Domain;

namespace DataAccessInterface
{
    public interface IPurchaseRepository
    {
        public Purchase CreatePurchase(Purchase purchase);
    }
}