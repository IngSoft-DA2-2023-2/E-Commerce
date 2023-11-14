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
    public class CategoryControllerTest
    {
        [TestMethod]
        public void GetAllCategories()
        {
            Category cat = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "category"
            };
            List<string> ret = new List<string>() { "category" };
            Mock<ICategoryLogic> categoryLogic = new Mock<ICategoryLogic>();
            categoryLogic.Setup(p => p.GetCategories()).Returns(ret);
            CategoryController categoryController = new CategoryController(categoryLogic.Object);
            var result = categoryController.GetAllCategories() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(ret, result.Value);
        }
    }
}