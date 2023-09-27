using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace UnitTest.WebApiModelsTest.Controller 
{ 

   [TestClass]
    public class PurchaseControllerTest
    {
        [TestMethod]
        public void CreateNewPurchase()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            Guid id = Guid.NewGuid();
            Guid buyer = Guid.NewGuid();
            List <CreateProductRequest> cart = new List<CreateProductRequest>()
            {
                new CreateProductRequest()
                {
                    Name = "name",
                    Description = "description",
                }
            };
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "name",
                    Description = "description",

                }
            };
            CreatePurchaseRequest purchaseRequest = new CreatePurchaseRequest()
            {
                Buyer = buyer,
                Cart = cart
            };
            Purchase purchase = new Purchase()
            {
                Id = id,
                BuyerId = buyer,
                Cart = products
            };
            Mock<IPurchaseLogic> mock = new Mock<IPurchaseLogic>();
            mock.Setup(p => p.CreatePurchase(It.Is<Purchase>(purchase => purchase.BuyerId == purchaseRequest.Buyer &&
                  purchase.Cart.First().Name == purchaseRequest.Cart.First().Name))).Returns(purchase);
            PurchaseController purchaseController = new PurchaseController(mock.Object);
            var result = purchaseController.CreatePurchase(purchaseRequest) as OkObjectResult;
            Assert.IsNotNull(result);
            var response = result.Value as CreatePurchaseResponse;
            Assert.AreEqual(purchaseRequest.Buyer, response.BuyerId);
            Assert.AreEqual(purchaseRequest.Cart.First().Name, response.Cart.First().Name);   
        }

        [TestMethod]
        public void GetAllPurchaseOk()
        {
            Guid buyerId = Guid.NewGuid();
            List<Purchase> purchases = new List<Purchase>();
            purchases.Add(new Purchase()
            {
                Id = Guid.NewGuid(),
                BuyerId = buyerId,
                Cart = new List<Product>()
                {
                    new Product()
                    {
                        Name = "product1",
                        Description = "description1"
                    }
                }
            });
            Mock<IPurchaseLogic> mock = new Mock<IPurchaseLogic>();
            mock.Setup(p => p.GetPurchases(null)).Returns(purchases);
            PurchaseController productController = new PurchaseController(mock.Object);
            var result = productController.GetAllPurchases() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(purchases, result.Value);
        }
    }
}

