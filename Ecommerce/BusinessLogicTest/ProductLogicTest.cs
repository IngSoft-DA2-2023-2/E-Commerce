using Domain;
using LogicInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterface;
using Domain.ProductParts;

namespace BusinessLogicTest
{
    [TestClass]
    public class ProductLogicTest
    {
        [TestMethod]
        public void CreateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand"},
                Category = new Category() { Name = "Category"},
                Color = new List<Colour>() { new Colour() { Name = "Colour"} }
            };

            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.CreateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object,brandRepo.Object,categoryRepo.Object,colourRepo.Object);
            var result = productLogic.AddProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name,expected.Name);
        }

        [TestMethod]
        public void UpdateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Color = new List<Colour>() { new Colour() { Name = "Colour" } }
            };

            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.UpdateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            var result = productLogic.UpdateProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name, expected.Name);
        }
    }
}
