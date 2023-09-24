using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
       public Product CreateProduct(Product product);
        void UpdateProduct(Product newProduct);
    }
}