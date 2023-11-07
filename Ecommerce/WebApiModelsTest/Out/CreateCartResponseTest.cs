using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.Out
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateCartResponseTest
    {
        [TestMethod]
        public void GivenCreateCartResponseReturnsItsId()
        {
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "name",
                    Price = 100,
                    Description = "description",
                    Brand = new Brand()
                    {
                        Name = "brand"
                    }, 
                    Category = new Category()
                    {
                        Name = "category"
                    },
                    Colours = new List<Colour>()
                    {
                        new Colour()
                        {
                            Name = "colour"
                        }
                    },
                }
            };
            Purchase purchase = new Purchase();
            purchase.Cart = products;
            purchase.CurrentPromotion = "promotion";
            purchase.Total = 100;
            
            CreateCartResponse createCartResponse = new CreateCartResponse(purchase);
            Assert.AreEqual(createCartResponse.Cart.First().Name, purchase.Cart.First().Name);
        }


    }
}
