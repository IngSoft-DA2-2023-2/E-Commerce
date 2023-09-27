using Domain;

namespace ApiModels.Out
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Colors { get; set; }

        public CreateProductResponse()
        {
        }
        public CreateProductResponse(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Brand = product.Brand;
            Category = product.Category;
            Colors = product.Color;
        }

    }
}
