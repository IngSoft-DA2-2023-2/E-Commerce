﻿using ApiModels.In;

namespace WebApiModelsTest.In
{
    [TestClass]
    public class CreatePurchaseRequestTest
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

    }
}