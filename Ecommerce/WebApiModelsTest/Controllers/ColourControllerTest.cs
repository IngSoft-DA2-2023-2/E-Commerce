using BusinessLogic;
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
    public class ColourControllerTest
    {

        [TestMethod]
        public void GetAllColours()
        {
            Colour colour = new Colour()
            {
                Id = Guid.NewGuid(),
                Name = "colour"
            };
            List<Colour> colours = new List<Colour>() { colour };
            Mock<IColourLogic> colourLogic = new Mock<IColourLogic>();
            colourLogic.Setup(p => p.GetColours()).Returns(colours);
            ColourController colourController = new ColourController(colourLogic.Object);
            var result = colourController.GetAllColours() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(colours, result.Value);
        }

    }
}
