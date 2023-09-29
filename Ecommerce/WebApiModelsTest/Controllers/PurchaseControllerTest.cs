using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using WebApi.Controllers;

namespace WebApiModelsTest.Controller
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
                    Brand = "brand",
                    Category = "category",
                    Color = color,

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
                    Color = new List<Colour>()
                    {
                        new Colour(){Name = "Red"},
                        new Colour(){Name = "Blue"}
                    },
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


           

            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                    Guid = buyer
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Guid);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);


            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.CreatePurchase(It.Is<Purchase>(purchase => purchase.BuyerId == purchaseRequest.Buyer &&
                  purchase.Cart.First().Name == purchaseRequest.Cart.First().Name))).Returns(purchase);
            PurchaseController purchaseController = new PurchaseController(purchaseLogic.Object, userLogic.Object);
            var result = purchaseController.CreatePurchase(purchaseRequest, guid.ToString()) as OkObjectResult;
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

            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                    Guid = buyerId
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Guid);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            

            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.GetPurchase(buyerId)).Returns(purchases);
            PurchaseController purchaseController = new PurchaseController(purchaseLogic.Object, userLogic.Object);
            var result = purchaseController.GetAllPurchases(guid.ToString()) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(purchases, result.Value);
        }

        [TestMethod]
        public void CreateNewPurchaseUnauthorized()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            Guid id = Guid.NewGuid();
            Guid buyer = Guid.NewGuid();
            List<CreateProductRequest> cart = new List<CreateProductRequest>()
            {
                new CreateProductRequest()
                {
                    Name = "name",
                    Description = "description",
                    Brand = "brand",
                    Category = "category",
                    Color = color,

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
                    Color = new List<Colour>()
                    {
                        new Colour(){Name = "Red"},
                        new Colour(){Name = "Blue"}
                    },
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




            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<string>{"admin"},
                    Guid = buyer
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Guid);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(false);


            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.CreatePurchase(It.Is<Purchase>(purchase => purchase.BuyerId == purchaseRequest.Buyer &&
                  purchase.Cart.First().Name == purchaseRequest.Cart.First().Name))).Returns(purchase);
            PurchaseController purchaseController = new PurchaseController(purchaseLogic.Object, userLogic.Object);
            Assert.ThrowsException<UnauthorizedAccessException>(() => purchaseController.CreatePurchase(purchaseRequest, guid.ToString()));

        }

        [TestMethod]
        public void GetAllPurchaseUnauthorized()
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

            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                    Guid = buyerId
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Guid);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == buyerId.ToString()))).Returns(false);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == buyerId.ToString()))).Returns(false);


            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();

            purchaseLogic.Setup(p => p.GetPurchase(buyerId)).Returns(purchases);
            purchaseLogic.Setup(p => p.GetAllPurchases()).Returns(purchases);

            PurchaseController purchaseController = new PurchaseController(purchaseLogic.Object, userLogic.Object);
            Assert.ThrowsException<UnauthorizedAccessException>(() => purchaseController.GetAllPurchases(buyerId.ToString()));

        }

    }
}

