using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
       public Product CreateProduct(Product product);
        public Product UpdateProduct(Product newProduct);
    }
}