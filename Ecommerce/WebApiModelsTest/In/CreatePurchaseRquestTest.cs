﻿using ApiModels.In;
using WebApi.Models.In;

namespace WebApiModelsTest.In
{
    [TestClass]
    public class CreatePurchaseRquestTest
    {
        private CreatePurchaseRequest purchaseRequest;
        Guid guid = Guid.NewGuid();
        private List<CreateProductRequest> products = new List<CreateProductRequest>()
        { 
            new CreateProductRequest()
            {
                Name = "name",
            }
        };

        [TestInitialize]
        public void Init()
        {
            purchaseRequest = new CreatePurchaseRequest();
        }
        [TestMethod]
        public void GivenPurchaseRequestReturnsProductRequest()
        {
            purchaseRequest.Cart = products;
            Assert.AreEqual(purchaseRequest.Cart.First().Name, products.First().Name);
        }

        [TestMethod]
        public void GivenPurchaseRequestReturnsBuyerId()
        {
            purchaseRequest.Id = guid;
            Assert.AreEqual(purchaseRequest.Id, guid);
        }

    }
}