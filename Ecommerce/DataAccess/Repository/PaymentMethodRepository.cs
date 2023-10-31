using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;

namespace DataAccess.Repository
{
    public class PaymentMethodRepository : IPaymentMethodRepository
    {
        private readonly ECommerceContext _context;
        public PaymentMethodRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForPaymentMethod(string name)
        {
            var bankDebit = _context.PaymentMethods.FirstOrDefault(b => b.CategoryName.Equals(name));
            if (bankDebit is null) throw new DataAccessException($"{name} does not exists");
            return true;
        }
    }
}
