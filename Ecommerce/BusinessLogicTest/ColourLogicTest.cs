using BusinessLogic;
using DataAccessInterface;
using Domain.ProductParts;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ColourLogicTest
    {
        [TestMethod]
        public void GivenExistingColourReturnsTrue()
        {
            Colour expected = new Colour() { Name = "Colour" };

            Mock<IColourRepository> repository = new Mock<IColourRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForColour("Colour")).Returns(true);
            var colourLogic = new ColourLogic(repository.Object);
            var result = colourLogic.CheckForColour(expected);
            repository.VerifyAll();
            Assert.IsTrue(result);
        }
    }
}
