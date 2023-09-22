using Domain;

namespace DataAccessInterface
{
    public interface IProductRepository
    {
       public Product CreateProduct(Product product);

       public IEnumerable<Product> GetProducts(string? name, string? brandName, string? categoryName);
    }
}