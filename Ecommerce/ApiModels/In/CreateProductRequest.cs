﻿using Domain;
using Domain.ProductParts;

namespace ApiModels.In
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Colours { get; set; }
        public int Stock { get; set; }
        public bool IncludeForPromotion { get; set; }

        public Product ToEntity()
        {
            List<Colour> colours = new List<Colour>();
            foreach (string colour in Colours) colours.Add(new Colour { Name = colour });
            return new Product
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Brand = new Brand()
                {
                    Name = Brand,
                },
                Category = new Category
                {
                    Name = Category,
                },
                Colours = colours,
                Id = Guid.NewGuid(),
                Stock = Stock,
                IncludeForPromotion = IncludeForPromotion
            };
        }
    }
}