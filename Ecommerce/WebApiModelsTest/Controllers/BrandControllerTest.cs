using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;

namespace WebApiModelsTest.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BrandControllerTest
    {
        [TestMethod]
        public void GetAllBrands()
        {
            Brand brand = new Brand()
            {
                Id = Guid.NewGuid(),
                Name = "brand"
            };

            List<string> brands = new List<string>() { "brand" };
            Mock<IBrandLogic> brandLogic = new Mock<IBrandLogic>();
            brandLogic.Setup(p => p.GetBrands()).Returns(brands);
            BrandController brandController = new BrandController(brandLogic.Object);
            var result = brandController.GetAllBrands() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(brands, result.Value);
        }
    }
}