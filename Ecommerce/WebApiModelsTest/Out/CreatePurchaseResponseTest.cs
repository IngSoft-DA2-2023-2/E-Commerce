using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Out;

namespace WebApiModelsTest.Out
{
    [TestClass]
    public class CreatePurchaseResponseTest
    {
        public Guid Id = Guid.NewGuid();
        public Guid BuyerId = Guid.NewGuid();
        public CreatePurchaseResponse createPurchaseResponse;
        Purchase purchase;
        List<Product> products;

        [TestInitialize]
        public void Init()
        {
            products = new List<Product>()
            { 
                new Product()
                {
                    Name = "Test",
                }
            };
        }

        [TestMethod]
        public void GivenProductResponseReturnsGuid()
        {
            purchase = new Purchase()
            {
                Id = Id,
                BuyerId = BuyerId,
                Cart = products
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(Id, createPurchaseResponse.Id);
        }
        [TestMethod]
        public void GivenProductResponseReturnsBuyerGuid()
        {
            purchase = new Purchase()
            {
                Id = Id,
                BuyerId = BuyerId,
                Cart = products
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(BuyerId, createPurchaseResponse.BuyerId);
        }





    }
}
