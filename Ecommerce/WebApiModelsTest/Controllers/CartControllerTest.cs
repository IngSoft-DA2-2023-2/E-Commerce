using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;

namespace WebApiModelsTest.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public void CreatePurchaseCart()
        {
            List<string> colour = new List<string>() { "Red", "Blue" };
            List<CreateProductRequest> cart = new List<CreateProductRequest>()
            {
                new CreateProductRequest()
                {
                    Name = "name",
                    Description = "description",
                    Brand = "brand",
                    Category = "category",
                    Colours = colour,
                    Stock = 1
                }
            };
            List<Product> products = new List<Product>()
            {
                new Product()
                {
                    Name = "name",
                    Description = "description",
                    Brand = new Brand{ Name = "brand"},
                    Category = new Category{ Name = "category"},
                    Colours = new List < Colour > () { new Colour() { Name = "Red" }, new Colour() { Name = "Red" } },
                    Stock = 1
                }
            };

            CreateCartRequest cartRequest = new CreateCartRequest()
            {
                Cart = cart,
            };
            Purchase purchase = new Purchase()
            {
                Cart = products,
            };

            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.CreatePurchaseLogic(It.Is<Purchase>(purchase => purchase.Cart.First().Name == cartRequest.Cart.First().Name))).Returns(purchase);
            CartController cartController = new CartController(purchaseLogic.Object);
            var result = cartController.CreateCart(cartRequest) as OkObjectResult;
            Assert.IsNotNull(result);
            var response = result.Value as CreateCartResponse;
            Assert.AreEqual(cartRequest.Cart.First().Name, response.Cart.First().Name);
        }
    }
}