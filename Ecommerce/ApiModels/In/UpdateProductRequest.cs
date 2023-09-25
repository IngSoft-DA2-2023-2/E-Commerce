using Domain;

namespace WebApi.Models.In
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public List<string> Color { get; set; }
        public Product ToEntity(Guid id)
        {
            return new Product {Id = id, Name = Name, Description = Description, Price = Price, Brand = Brand, Category = Category, Color = Color };
        }
    }
}
