using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        public Product AddProduct(Product newProduct);
        public Product GetProductById(Guid id);
        public IEnumerable<Product> FilterUnionProduct(string? name,  string? brandName, string? categoryName);
        public Product UpdateProduct(Product newProduct);


    }
}
