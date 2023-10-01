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
            List<string> colour = new List<string>() { "Red", "Blue" };
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
                    Colour = colour,

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
                    Colours = new List < Colour > () { new Colour() { Name = "Red" }, new Colour() { Name = "Red" } }
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
                UserId = buyer,
                Cart = products
            };


           

            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List < StringWrapper > { new StringWrapper() { Info = "buyer" } },
                    Id = buyer
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Id);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);


            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.CreatePurchase(It.Is<Purchase>(purchase => purchase.UserId == purchaseRequest.Buyer &&
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
                UserId = buyerId,
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
                    Roles=new List<StringWrapper>{new StringWrapper() { Info = "buyer" } },
                    Id = buyerId
                },
            };

            Guid guid = Guid.NewGuid();
            Session session = new Session() { Id = guid, User = listUsers.First() };

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Id);
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
            List<string> colour = new List<string>() { "Red", "Blue" };
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
                    Colour = colour,

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
                    Colours = new List<Colour>()
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
                UserId = buyer,
                Cart = products
            };




            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper { Id = new Guid(), Info =  "buyer" } },
                    Id = buyer
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Id);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(false);


            Mock<IPurchaseLogic> purchaseLogic = new Mock<IPurchaseLogic>();
            purchaseLogic.Setup(p => p.CreatePurchase(It.Is<Purchase>(purchase => purchase.Id == purchaseRequest.Buyer &&
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

                UserId = buyerId,
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
                    Roles=new List<StringWrapper>{ new StringWrapper { Id = new Guid(), Info =  "buyer" } },
                    Id = buyerId
                },
            };

            Guid guid = Guid.NewGuid();

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(listUsers.First().Id);
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

