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
    public class PaganzaRepository : IPaganzaRepository
    {
        private readonly ECommerceContext _context;
        public PaganzaRepository(ECommerceContext context)
        {
            _context = context;
        }
        public bool CheckForPaganza(string Paganza)
        {
            var paganza = _context.Paganzas.FirstOrDefault(b => b.CategoryName.Equals(Paganza));
            if (paganza is null) throw new DataAccessException($"{Paganza} does not exists");
            return true;
        }
    }
}
