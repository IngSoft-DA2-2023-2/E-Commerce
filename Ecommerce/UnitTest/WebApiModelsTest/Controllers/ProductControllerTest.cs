using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.In;
using WebApi.Controllers;
using LogicInterface;
using Moq;
using Domain;
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
        public void GetAllProductsOk()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product() { Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue " }, Price = 200 });
            products.Add(new Product() { Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue " }, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters().Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsInternalServerError()
        {
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null))).Throws(new Exception());
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters().Result as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public void GetAllProductsEmpty()
        {
            List<Product> products = new List<Product>();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters().Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithNameOk()
        {
            const string exceptedName = "Name1";
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product() { Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue " }, Price = 200 });
            products.Add(new Product() { Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue " }, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == exceptedName))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(name:exceptedName).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }


    }
}
