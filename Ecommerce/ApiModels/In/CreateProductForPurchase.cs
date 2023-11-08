using Domain.ProductParts;
using Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ApiModels.In
{
    public class CreateProductForPurchase
    { 
    public Guid Id {  get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }
    public List<string> Colour { get; set; }
    public int Stock { get; set; }

    public Product ToEntity()
    {
        List<Colour> colours = new List<Colour>();
        foreach (string colour in Colour) colours.Add(new Colour { Name = colour });
        return new Product
        {
            Id = Id,
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
            Stock = Stock
        };
    }
}
}
