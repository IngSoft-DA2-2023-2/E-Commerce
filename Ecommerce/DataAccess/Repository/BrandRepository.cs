﻿using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;

namespace DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ECommerceContext _context;
        public BrandRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForBrand(string brandName)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Name.Equals(brandName));
            if (brand is null) throw new DataAccessException($"Brand {brandName} does not exists");
            return true;
        }
    }
}