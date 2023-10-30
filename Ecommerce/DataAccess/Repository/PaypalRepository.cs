using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class PaypalRepository : IPaypalRepository
    {
        private readonly ECommerceContext _context;

        public PaypalRepository(ECommerceContext context)
        {
            _context = context;
        }
        public bool CheckForPaypal(string paypalName)
        {
            var paypal = _context.Paypal.FirstOrDefault(b => b.CategoryName.Equals(paypalName));
            if (paypal is null) throw new DataAccessException($"{paypalName} does not exists");
            return true;
        }
    }
}
