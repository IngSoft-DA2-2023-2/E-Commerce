﻿using DataAccess.Context;
using DataAccess.Exceptions;
using DataAccessInterface;
using Domain;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext _eCommerceContext;
        public ProductRepository(ECommerceContext context)
        {
            _eCommerceContext = context;
        }



        public Product CreateProduct(Product product)
        {
            if (_eCommerceContext.Products.FirstOrDefault(p => p.Name.Equals(product.Name)) is null)
            {
                _eCommerceContext.Products.Add(product);
                _eCommerceContext.SaveChanges();
                return product;
            }
            throw new DataAccessException($"Product {product.Name} already exists.");
        }
    }
}