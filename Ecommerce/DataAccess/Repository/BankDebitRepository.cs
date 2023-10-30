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
    public class BankDebitRepository : IBankDebitRepository
    {
        private readonly ECommerceContext _context;
        public BankDebitRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForBankDebit(string bankName)
        {
            var bankDebit = _context.BankDebits.FirstOrDefault(b => b.Bank.Equals(bankName));
            if (bankDebit is null) throw new DataAccessException($"{bankName} does not exists");
            return true;
        }
    }
}
