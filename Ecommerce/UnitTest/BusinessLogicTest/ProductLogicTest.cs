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

namespace UnitTest.BusinessLogicTest
{
    [TestClass]
    public class ProductLogicTest
    {
        [TestMethod]
        public void CreateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample"
            };

            Mock<IProductRepository> repository = new Mock<IProductRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CreateProduct(It.IsAny<Product>())).Returns(expected);
            var productLogic = new ProductLogic(repository.Object);
            var result = productLogic.AddProduct(expected);
            repository.VerifyAll();
            Assert.AreEqual(result.Name,expected.Name);
        }

        [TestMethod]
        public void UpdateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample"
            };

            Mock<IProductRepository> repository = new Mock<IProductRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.UpdateProduct(It.IsAny<Product>())).Returns(expected);
            var productLogic = new ProductLogic(repository.Object);
            var result = productLogic.UpdateProduct(expected);
            repository.VerifyAll();
            Assert.AreEqual(result.Name, expected.Name);
        }
    }
}
