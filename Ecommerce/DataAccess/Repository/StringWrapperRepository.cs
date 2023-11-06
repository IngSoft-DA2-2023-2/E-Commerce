using DataAccess.Context;
using DataAccessInterface;
using Domain.ProductParts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class StringWrapperRepository : IStringWrapperRepository
    {
        private readonly ECommerceContext _context;
        public StringWrapperRepository(ECommerceContext context)
        {
            _context = context;
        }
       
        public IEnumerable<StringWrapper> GetRoles()
        {
           return _context.StringListWrappers.ToList();
        }
    }
}
