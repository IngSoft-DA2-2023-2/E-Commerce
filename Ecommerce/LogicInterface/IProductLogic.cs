using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        public Product AddProduct(Product newProduct);
        public Product GetProductById(Guid id);
        public IEnumerable<Product> FilterUnionProduct(string? name, string? brandName, string? categoryName, string? priceRange);
        public IEnumerable<Product> FilterIntersectionProduct(string? name, string? brandName, string? categoryName, string? priceRange);
        public Product UpdateProduct(Product newProduct);
        public bool CheckProduct(Product product);


    }
}
