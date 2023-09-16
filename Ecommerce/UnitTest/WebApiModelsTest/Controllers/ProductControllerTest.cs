using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.In;
using WebApi.Controllers;
using WebApi.LogicInterface;
using Moq;
using WebApi.Domain;
using Microsoft.AspNetCore.Mvc;

namespace UnitTest.WebApiModelsTest.Controller
{

    [TestClass]
    public class ProductControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            
        }

        [TestMethod]
        public void GetAllProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product()  {Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue "}, Price = 200 });
            products.Add(new Product() { Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue "}, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts()).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProducts().Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }


    }
}
