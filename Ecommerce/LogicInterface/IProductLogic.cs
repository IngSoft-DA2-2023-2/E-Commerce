using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        Guid AddProduct(Product newProduct);
        public List<Product> GetProducts(string? name, string? brandName, string? categoryName);

    }
}
