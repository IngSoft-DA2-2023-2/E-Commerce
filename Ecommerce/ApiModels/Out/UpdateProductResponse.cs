using Domain;

namespace ApiModels.Out
{
    public class UpdateProductResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Colors { get; set; }

        public UpdateProductResponse()
        {
        }
        public UpdateProductResponse(Product product)
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
