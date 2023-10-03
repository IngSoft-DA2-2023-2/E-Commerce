using DataAccess.Context;
using DataAccessInterface.Exceptions;
using DataAccessInterface;
using Domain;
using Domain.ProductParts;
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
            if (_eCommerceContext.Products.FirstOrDefault(p => p.Name.Equals(product.Name)) is null)
            {
                _eCommerceContext.Products.Add(product);
                _eCommerceContext.SaveChanges();
                return product;
            }
            throw new DataAccessException($"Product {product.Name} already exists.");
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
                return selectedProducts;
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
                return selectedProducts;
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
                return selectedProducts;
            }
        }

        public Product UpdateProduct(Product newProduct)
        {
            if(_eCommerceContext.Products.First(p=> p.Id.Equals(newProduct.Id)) is null)
            {
                throw new DataAccessException($"Product {newProduct.Name} does not exist.");
            }
            else
            {
                _eCommerceContext.Products.Update(newProduct);
                _eCommerceContext.SaveChanges();
                return newProduct;
            }
        }
    }
}