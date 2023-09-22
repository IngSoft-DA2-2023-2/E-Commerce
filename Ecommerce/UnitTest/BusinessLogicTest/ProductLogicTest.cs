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
        [TestMethod] public void GetAllProducts()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            Product test = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Sample",
                Description = "Description",
                Brand = "Brand",
                Category = "Category",
                Color = color
            };
            IEnumerable<Product> expected = new List<Product>()
            {
                test
            };
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(logic => logic.GetProducts(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(expected);
            var productLogic = new ProductLogic(repository.Object);
            var result = productLogic.GetProducts(null,null, null);
            repository.VerifyAll();
            Assert.AreEqual(result.First().Name, expected.First().Name);
        }
    }
}
