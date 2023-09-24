using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
       public Product CreateProduct(Product product);
        Product? GetProductById(Guid id);
        public Product UpdateProduct(Product newProduct);
    }
}