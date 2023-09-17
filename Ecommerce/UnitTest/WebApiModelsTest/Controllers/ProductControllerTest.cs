using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using WebApi.Models.In;
using WebApi.Controllers;
using LogicInterface;
using Moq;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.Out;

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
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters().Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsInternalServerError()
        {
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Throws(new Exception());
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
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
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
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == exceptedName),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(name: exceptedName).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithBrandNameOk()
        {
            const string exceptedBrandName = "Brand1";
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product() { Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue " }, Price = 200 });
            products.Add(new Product() { Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue " }, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == exceptedBrandName),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(brandName: exceptedBrandName).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithCategoryNameOk()
        {
            const string exceptedCategoryName = "Category1";
            List<Product> products = new List<Product>();
            products.Add(new Product() { Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product() { Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue " }, Price = 200 });
            products.Add(new Product() { Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue " }, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == exceptedCategoryName))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(categoryName: exceptedCategoryName).Result as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void CreateNewProduct()
        {
            CreateProductRequest productRequest = new CreateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = new List<string>() { "Red", "Blue" },
                Price = 100
            };
            Product product = new Product()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = new List<string>() { "Red", "Blue" },
                Price = 100
            };
            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.AddProduct(It.Is<Product>(product => product.Name == productRequest.Name &&
              product.Description == productRequest.Description &&
               product.Category == productRequest.Category &&
                product.Brand == productRequest.Brand && product.Color == productRequest.Color &&
                 product.Price == productRequest.Price))).Returns(guid);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.CreateProduct(productRequest).Result as OkObjectResult;
            
            Assert.IsNotNull(result);
            var response = result.Value as CreateProductResponse;
            Assert.AreEqual(guid, response.Id);
            Assert.AreEqual(productRequest.Name, response.Name);
            Assert.AreEqual(productRequest.Description, response.Description);
            Assert.AreEqual(productRequest.Category, response.Category);
            Assert.AreEqual(productRequest.Brand, response.Brand);
            Assert.AreEqual(productRequest.Color, response.Colors);
            Assert.AreEqual(productRequest.Price, response.Price);

        }




    }
}
