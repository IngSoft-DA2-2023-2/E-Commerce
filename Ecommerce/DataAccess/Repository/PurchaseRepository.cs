using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;

namespace DataAccess.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ECommerceContext _eCommerceContext;
        public PurchaseRepository(ECommerceContext eCommerce)
        {
            _eCommerceContext = eCommerce;
        }
        public Purchase CreatePurchase(Purchase purchase)
        {
            if (_eCommerceContext.Purchases.FirstOrDefault(p => p.Id.Equals(purchase.Id)) is null)
            {
                _eCommerceContext.Purchases.Add(purchase);
                _eCommerceContext.SaveChanges();
                return purchase;
            }
            throw new DataAccessException($"Purchase already exists.");

        }

        public IEnumerable<Purchase> GetAllPurchases()
        {
            return _eCommerceContext.Purchases.ToList();
        }

        public IEnumerable<Purchase> GetPurchase(Guid id)
        {
            return _eCommerceContext.Purchases.Where(p => p.UserId == id).ToList();
        }

    }
}