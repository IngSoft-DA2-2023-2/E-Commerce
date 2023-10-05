using Domain;
using Domain.ProductParts;

namespace ApiModels.In
{
    public class UpdateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

        public List<string> Colour { get; set; }
        public Product ToEntity(Guid id)
        {
            List<Colour> colours = new List<Colour>();
            foreach (string colour in Colour) colours.Add(new Colour() { Name = colour });
            return new Product
            {
                Id = id,
                Name = Name,
                Description = Description,
                Price = Price,
                Brand = new Brand() { Name = Brand },
                Category = new Category() { Name = Category },
                Colours = colours
            };
        }
    }
}
