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
    public class StringWrapperControllerTest
    {
        [TestMethod]
        public void GetAllRoles()
        {
            StringWrapper stringWrapper = new StringWrapper()
            {
                Id = Guid.NewGuid(),
                Info = "admin"
            };
            List<StringWrapper> stringWrappers = new List<StringWrapper>() { stringWrapper };
            IEnumerable<string> expected = new List<string> { "admin" };
            Mock<IStringWrapperLogic> stringWrapperLogic = new Mock<IStringWrapperLogic>();
            stringWrapperLogic.Setup(p => p.GetRoles()).Returns(stringWrappers);
            StringWrapperController stringWrapperController = new StringWrapperController(stringWrapperLogic.Object);
            var result = stringWrapperController.GetAllRoles() as OkObjectResult;
            var resultValue = result.Value as IEnumerable<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.First(), resultValue.First());
            Assert.AreEqual(expected.Count(), resultValue.Count());
        }
    }
}