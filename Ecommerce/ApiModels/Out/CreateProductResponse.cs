using Domain;
using Domain.ProductParts;

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
        public List<string> Colours { get; set; }

        public CreateProductResponse()
        {
        }
        public CreateProductResponse(Product product)
        {
            List<string> colours = new List<string>();
            foreach(Colour colour in product.Colours)
            {
                colours.Add(colour.Name);
            }
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Description = product.Description;
            Brand = product.Brand.Name;
            Category = product.Category.Name;
            Colours = colours;
        }

    }
}
