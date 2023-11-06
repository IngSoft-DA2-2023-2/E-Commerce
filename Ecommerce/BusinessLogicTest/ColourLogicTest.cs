using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
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
        [TestMethod]
        public void GivenNonExistingColourThrowsException()
        {
            Colour expected = new Colour() { Name = "Colour" };

            Mock<IColourRepository> repository = new Mock<IColourRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForColour("Colour")).Throws(new DataAccessException("Colour Colour does not exists"));
            var colourLogic = new ColourLogic(repository.Object);
            Exception catchedException = null;
            try
            {
                colourLogic.CheckForColour(expected);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.IsTrue(catchedException?.Message.Equals("Colour Colour does not exists"));
        }

        [TestMethod]
        public void GivenExistingColourReturnsColour()
        {
            Colour colour = new Colour() { Name = "colour" };
            var colourContext = new Mock<IColourRepository>();
            colourContext.Setup(ctx => ctx.GetColours()).Returns(new List<Colour>() { colour });
            IColourLogic colourLogic = new ColourLogic(colourContext.Object);
            var expectedReturn = colourLogic.GetColours();
            Assert.IsTrue(expectedReturn.Contains(colour));
        }
    }
}
