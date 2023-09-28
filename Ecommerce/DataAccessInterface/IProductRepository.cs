using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
        public Product CreateProduct(Product product);
        public Product GetProductById(Guid id);
        public IEnumerable<Product> GetProductByName(string name);
        public Product UpdateProduct(Product newProduct);
    }
}