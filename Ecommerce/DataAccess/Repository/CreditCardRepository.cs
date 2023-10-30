using DataAccess.Context;
using DataAccessInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CreditCardRepository : ICreditCardRepository
    {
        private readonly ECommerceContext _context;
        public CreditCardRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForCreditCard(string Flag)
        {
            var creditCard = _context.CreditCards.FirstOrDefault(b => b.Flag.Equals(Flag));
            if (creditCard is null) throw new DataAccessException($"CreditCard {Flag} does not exists");
            return true;
        }
    }
}
