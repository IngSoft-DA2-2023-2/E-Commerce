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
    public class ColourControllerTest
    {
        [TestMethod]
        public void GetAllColours()
        {
            string name = "colour";
            Colour colour = new Colour()
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            List<Colour> colours = new List<Colour>() { colour };
            IEnumerable<string> expected = new List<string>() { name };
            Mock<IColourLogic> colourLogic = new Mock<IColourLogic>();
            colourLogic.Setup(p => p.GetColours()).Returns(colours);
            ColourController colourController = new ColourController(colourLogic.Object);
            var result = colourController.GetAllColours() as OkObjectResult;
            var resultValue = result.Value as IEnumerable<string>;
            Assert.IsNotNull(result);
            Assert.AreEqual(expected.First(), resultValue.First());
            Assert.AreEqual(expected.Count(), resultValue.Count());
        }
    }
}