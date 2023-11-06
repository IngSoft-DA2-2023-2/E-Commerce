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
            var roles = _context.StringListWrappers.ToList();
            List<StringWrapper> rolesReturn = new List<StringWrapper>();
            foreach (StringWrapper role in roles)
            {
                if (!(rolesReturn.Contains(role)))
                {
                    rolesReturn.Add(role);
                }
            }
           return rolesReturn;
        }
    }
}
