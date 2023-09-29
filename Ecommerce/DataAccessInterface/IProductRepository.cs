using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
        public Product CreateProduct(Product product);
        public IEnumerable<Product> GetProductByBrand(string name);
        public Product GetProductById(Guid id);
        public IEnumerable<Product> GetProductByName(string name);
        public Product UpdateProduct(Product newProduct);
    }
}