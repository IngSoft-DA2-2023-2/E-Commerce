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
           
            _eCommerceContext.Purchases.Add(purchase);
            _eCommerceContext.SaveChanges();
            return purchase;
         
        }

        public IEnumerable<Purchase> GetPurchases(Guid? id)
        {
            throw new NotImplementedException();
        }
    }
}