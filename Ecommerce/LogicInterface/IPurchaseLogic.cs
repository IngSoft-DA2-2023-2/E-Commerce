using Domain;

namespace LogicInterface
{
    public interface IPurchaseLogic
    {
        public Purchase CreatePurchase(Purchase purchase);
        public IEnumerable<Purchase> GetPurchases(Guid? id);
    }
}
