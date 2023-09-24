using DataAccess.Context;
using DataAccessInterface.Exceptions;
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

        public Product GetProductById(Guid id)
        {
            var product = _eCommerceContext.Products.FirstOrDefault(p => p.Id.Equals(id));
            if (product is null)
            {
                throw new DataAccessException($"Product with id {id} does not exist.");
            }
            else
            {
                return product;
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