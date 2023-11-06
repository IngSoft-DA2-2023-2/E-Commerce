using Domain.ProductParts;
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

namespace WebApiModelsTest.Controllers
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringWrapperControllerTest
    {

        [TestMethod]
        public void GetAllRoles()
        {

            StringWrapper stringWrapper = new StringWrapper()
            {
                Id = Guid.NewGuid(),
                Info = "Admin"
            };
            List<StringWrapper> stringWrappers = new List<StringWrapper>() { stringWrapper };
            Mock<IStringWrapperLogic> stringWrapperLogic = new Mock<IStringWrapperLogic>();
            stringWrapperLogic.Setup(p => p.GetRoles()).Returns(stringWrappers);
            StringWrapperController stringWrapperController = new StringWrapperController(stringWrapperLogic.Object);
            var result = stringWrapperController.GetAllRoles() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(stringWrappers, result.Value);
        }
    }
}
