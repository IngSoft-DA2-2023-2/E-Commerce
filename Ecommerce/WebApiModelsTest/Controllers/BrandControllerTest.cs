using Domain.ProductParts;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using BusinessLogic;

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

            List<Brand> brands = new List<Brand>() { brand };
            Mock<IBrandLogic> brandLogic = new Mock<IBrandLogic>();
            brandLogic.Setup(p => p.GetBrands()).Returns(brands);
            BrandController brandController = new BrandController(brandLogic.Object);
            var result = brandController.GetAllBrands() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(brands, result.Value);
        }


    }
}
