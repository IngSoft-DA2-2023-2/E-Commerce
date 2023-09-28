using Domain;

namespace ApiModels.In
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Color { get; set; }

        public Product ToEntity()
        {
            return new Product { Name = Name, Description = Description, Price = Price, Brand = Brand, Category = Category, Color = Color };
        }
    }
}
