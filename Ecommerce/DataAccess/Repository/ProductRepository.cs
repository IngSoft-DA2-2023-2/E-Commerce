using DataAccess.Context;
using DataAccessInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Domain.ProductParts;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using System.Security.Principal;
using System.Collections.Generic;

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
            var productos = _eCommerceContext.Products.
                Where(p => (p.Name.Equals(product.Name)))
                .FirstOrDefault();
            if (productos is null)
            {
                _eCommerceContext.Products.Add(product);
                _eCommerceContext.SaveChanges();
                return product;
            }
            throw new DataAccessException($"Product {product.Name} already exists.");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _eCommerceContext.Products.
                 Include(p => p.Brand).
                 Include(p => p.Category).
                 Include(p => p.Colours).ToList();

            var productsReturn = new List<Product>();
            foreach (Product product in products)
            {
                if(!(productsReturn.Contains(product)))
                {
                    productsReturn.Add(product);
                }
            }

            return productsReturn;
        }
     

        

        public IEnumerable<Product> GetProductByBrand(string brand)
        {

            var selectedProducts = _eCommerceContext.Products.
                Include(p => p.Brand).
                Include(p => p.Category).
                Include(p => p.Colours).
                Where(p => p.Brand.Name == brand).
                ToList();
            if (!selectedProducts.Any())
            {
                throw new DataAccessException($"Product brand {brand} does not exist.");
            }
            else
            {
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!(productsReturn.Contains(product)))
                    {
                        productsReturn.Add(product);
                    }
                }

                return productsReturn;
            }
        }

        public IEnumerable<Product> GetProductByCategory(string category)
        {
            var selectedProducts = _eCommerceContext.Products.
                Include(p => p.Brand).
                Include(p => p.Category).
                Include(p => p.Colours).
                Where(p => p.Category.Name == category).
                ToList();

            if (!selectedProducts.Any())
            {
                throw new DataAccessException($"Product category {category} does not exist.");
            }
            else
            {
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!(productsReturn.Contains(product)))
                    {
                        productsReturn.Add(product);
                    }
                }

                return productsReturn;
            }
        }

        public Product GetProductById(Guid id)
        {

            var product = _eCommerceContext.Products.
               Include(p => p.Brand).
               Include(p => p.Category).
               Include(p => p.Colours).
               Where(p => p.Id == id).FirstOrDefault();
            if (product is null)
            {
                throw new DataAccessException($"Product with id {id} does not exist.");
            }
            else
            {
                return product;
            }
        }

        public IEnumerable<Product> GetProductByName(string name)
        {
            var selectedProducts = _eCommerceContext.Products.
                Include(p => p.Brand).
                Include(p => p.Category).
                Include(p => p.Colours).
                Where(p => p.Name == name).
                ToList();
            if (!selectedProducts.Any())
            {
                throw new DataAccessException($"Product {name} does not exist.");
            }
            else
            {
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!(productsReturn.Contains(product)))
                    {
                        productsReturn.Add(product);
                    }
                }

                return productsReturn;
            }
        }

        public Product UpdateProduct(Product newProduct)
        {
            var product = _eCommerceContext.Products.Include(p => p.Brand).
                Include(p => p.Category).
                Include(p => p.Colours).
                Where(p => p.Id == newProduct.Id).First();
            if (product is null)
            {
                throw new DataAccessException($"Product does not exist.");
            }
            else
            {
                if(newProduct.Name != null) product.Name = newProduct.Name;
                if (newProduct.Description != null) product.Description = newProduct.Description;
                if(newProduct.Price != null) product.Price = newProduct.Price;
                if(newProduct.Brand != null) product.Brand = newProduct.Brand;
                if (newProduct.Category != null) product.Category = newProduct.Category;
                if (newProduct.Colours != null) product.Colours = newProduct.Colours;

                  _eCommerceContext.SaveChanges();
                return newProduct;
            }
        }
    }
}