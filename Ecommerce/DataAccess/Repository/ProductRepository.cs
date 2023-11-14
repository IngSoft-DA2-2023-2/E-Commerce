using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

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
            var products = _eCommerceContext.Products.
                 Include(p => p.Brand).
                 Include(p => p.Category).
                 Include(p => p.Colours).ToList();

            if (products.Contains(product))
            {
                throw new DataAccessException($"Product {product.Name} already exists.");
            }
            else
            {
                products = null;
            }

            if (products is null)
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
            IEnumerable<Product> doNotInclude = GetProductsOfPurchases();
            foreach (Product product in products)
            {
                if (!doNotInclude.Any(p => p.Id == product.Id))
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
                return new List<Product>();
            }
            else
            {
                IEnumerable<Product> doNotInclude = GetProductsOfPurchases();
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!doNotInclude.Any(p => p.Id == product.Id))
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
                return new List<Product>();
            }
            else
            {
                IEnumerable<Product> doNotInclude = GetProductsOfPurchases();
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!doNotInclude.Any(p => p.Id == product.Id))
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
                Where(p => p.Name.Contains(name)).
                ToList();
            if (!selectedProducts.Any())
            {
                return new List<Product>();
            }
            else
            {
                IEnumerable<Product> doNotInclude = GetProductsOfPurchases();
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!doNotInclude.Any(p => p.Id == product.Id))
                    {
                        productsReturn.Add(product);
                    }
                }
                return productsReturn;
            }
        }

        public IEnumerable<Product> GetProductByPriceRange(string priceRange)
        {
            int from = int.Parse(priceRange.Split('-')[0]);
            int to = int.Parse(priceRange.Split('-')[1]);
            var selectedProducts = _eCommerceContext.Products.
            Include(p => p.Brand).
            Include(p => p.Category).
            Include(p => p.Colours).
            Where(p => p.Price <= to && p.Price >= from).
            ToList();
            if (!selectedProducts.Any())
            {
                return new List<Product>();
            }
            else
            {
                IEnumerable<Product> doNotInclude = GetProductsOfPurchases();
                var productsReturn = new List<Product>();
                foreach (Product product in selectedProducts)
                {
                    if (!doNotInclude.Any(p => p.Id == product.Id))
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
                if (newProduct.Name != null) product.Name = newProduct.Name;
                if (newProduct.Description != null) product.Description = newProduct.Description;
                if (newProduct.Price != 0) product.Price = newProduct.Price;
                if (newProduct.Brand != null) product.Brand = newProduct.Brand;
                if (newProduct.Category != null) product.Category = newProduct.Category;
                if (newProduct.Colours != null) product.Colours = newProduct.Colours;
                if (newProduct.Stock != 0) product.Stock = newProduct.Stock;
                if (newProduct.IncludeForPromotion != product.IncludeForPromotion) product.IncludeForPromotion = 
                        newProduct.IncludeForPromotion;

                _eCommerceContext.SaveChanges();
                return newProduct;
            }
        }

        public int UpdateStock(Product newProduct)
        {
            var product = _eCommerceContext.Products.Where(p => p.Id == newProduct.Id).FirstOrDefault();
            if (product is null)
            {
                throw new DataAccessException($"Product does not exist.");
            }
            product.Stock -= 1;
            _eCommerceContext.SaveChanges();
            return product.Stock;
        }

        private IEnumerable<Product> GetProductsOfPurchases()
        {
            var purchases = _eCommerceContext.Purchases.Include(p => p.Cart).ToList();
            IEnumerable<Product> products = new List<Product>();
            foreach (var purchase in purchases)
            {
                products = products.Union(purchase.Cart);
            }
            return products;

        }

    }
}