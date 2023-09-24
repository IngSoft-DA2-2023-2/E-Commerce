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
using System.Drawing;

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
            products.Add(new Product() { 
                Name = "Name1", Description = "Description1", Category = "Category1", Brand = "Brand1", Color = { "Red", "Blue" }, Price = 100 });
            products.Add(new Product() { 
                Name = "Name2", Description = "Description2", Category = "Category2", Brand = "Brand2", Color = { "Red", "Blue " }, Price = 200 });
            products.Add(new Product() { 
                Name = "Name3", Description = "Description3", Category = "Category3", Brand = "Brand3", Color = { "Red", "Blue " }, Price = 300 });
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProducts(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters() as OkObjectResult;
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
            var result = productController.GetAllProductsByFilters() as StatusCodeResult;
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
            var result = productController.GetAllProductsByFilters() as OkObjectResult;
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
            var result = productController.GetAllProductsByFilters(name: exceptedName) as OkObjectResult;
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
            var result = productController.GetAllProductsByFilters(brandName: exceptedBrandName) as OkObjectResult;
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
            var result = productController.GetAllProductsByFilters(categoryName: exceptedCategoryName) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }


        [TestMethod]
        public void CreateNewProduct()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            CreateProductRequest productRequest = new CreateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = color,
                Price = 100
            };
            Product product = new Product()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = color,
                Price = 100
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.AddProduct(It.Is<Product>(product => product.Name == productRequest.Name &&
              product.Description == productRequest.Description &&
               product.Category == productRequest.Category &&
                product.Brand == productRequest.Brand && product.Color == productRequest.Color &&
                 product.Price == productRequest.Price))).Returns(product);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.CreateProduct(productRequest) as OkObjectResult;          
            Assert.IsNotNull(result);
            var response = result.Value as CreateProductResponse;
            Assert.AreEqual(productRequest.Name, response.Name);
            Assert.AreEqual(productRequest.Description, response.Description);
            Assert.AreEqual(productRequest.Category, response.Category);
            Assert.AreEqual(productRequest.Brand, response.Brand);
            Assert.AreEqual(productRequest.Color, response.Colors);
            Assert.AreEqual(productRequest.Price, response.Price);
        }

        [TestMethod]
        public void CreateNewProductInternalServerError()
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
                                                                      product.Price == productRequest.Price))).Throws(new Exception());
            ProductController productController = new ProductController(mock.Object);
            var result = productController.CreateProduct(productRequest) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }


        [TestMethod]
        public void UpdateNewProduct()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            UpdateProductRequest productRequest = new UpdateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = color,
                Price = 100
            };
            Product product = new Product()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = color,
                Price = 100
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.UpdateProduct(It.Is<Product>(product => product.Name == productRequest.Name &&
                         product.Description == productRequest.Description &&
                                       product.Category == productRequest.Category &&
                                                      product.Brand == productRequest.Brand && product.Color == productRequest.Color &&
                                                                      product.Price == productRequest.Price))).Returns(product);
            ProductController productController = new ProductController(mock.Object);
            Guid id = new Guid();
            var result = productController.UpdateProduct(id,productRequest) as OkObjectResult;
            Assert.IsNotNull(result);
            var response = result.Value as UpdateProductResponse;
            Assert.AreEqual(productRequest.Name, response.Name);
            Assert.AreEqual(productRequest.Description, response.Description);
            Assert.AreEqual(productRequest.Category, response.Category);
            Assert.AreEqual(productRequest.Brand, response.Brand);
            Assert.AreEqual(productRequest.Color, response.Colors);
            Assert.AreEqual(productRequest.Price, response.Price);
        }

        [TestMethod]
        public void UpdateNewProductInternalServerError()
        {
            UpdateProductRequest productRequest = new UpdateProductRequest()
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
            mock.Setup(p => p.UpdateProduct(It.Is<Product>(product => product.Name == productRequest.Name &&
                                    product.Description == productRequest.Description &&
                                                                          product.Category == productRequest.Category &&
                                                                                                                               product.Brand == productRequest.Brand && product.Color == productRequest.Color &&
                                                                                                                                                                                                    product.Price == productRequest.Price))).Throws(new Exception());
            ProductController productController = new ProductController(mock.Object);
            var result = productController.UpdateProduct(guid, productRequest) as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public void GetProductByIdOk()
        {
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
            mock.Setup(p => p.GetProductById(It.Is<Guid>(id => id == guid))).Returns(product);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetProductById(guid) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.Value);
        }




    }
}
