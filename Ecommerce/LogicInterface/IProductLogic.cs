
using System.Linq;
using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        Product AddProduct(Product newProduct);
        Product GetProductById(Guid id);
        public List<Product> GetProducts(string? name,  string? brandName, string? categoryName);

        Product UpdateProduct(Product newProduct);


    }
}
