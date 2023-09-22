
using System.Linq;
using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        Product AddProduct(Product newProduct);
        public IEnumerable<Product> GetProducts(string? name,  string? brandName, string? categoryName);

    }
}
