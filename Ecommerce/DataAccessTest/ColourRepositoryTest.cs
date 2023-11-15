using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ColourRepositoryTest
    {
        private readonly string colourName = "colour";

        [TestMethod]
        public void GivenExistingColourNameReturnsTrue()
        {
            Colour colour = new Colour() { Name = "colour" };
            var colourContext = new Mock<ECommerceContext>();
            colourContext.Setup(ctx => ctx.Colours).ReturnsDbSet(new List<Colour>() { colour });
            IColourRepository colourRepository = new ColourRepository(colourContext.Object);
            var expectedReturn = colourRepository.CheckForColour(colourName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingColourNameThrowsException()
        {
            var colourContext = new Mock<ECommerceContext>();
            colourContext.Setup(ctx => ctx.Colours).ReturnsDbSet(new List<Colour>() { });
            IColourRepository colourRepository = new ColourRepository(colourContext.Object);
            Exception catchedException = null;
            try
            {
                colourRepository.CheckForColour(colourName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"Colour {colourName} does not exists"));
        }

        [TestMethod]
        public void GivenExistingColourReturnsColour()
        {
            Colour colour = new Colour() { Name = "colour" };
            var colourContext = new Mock<ECommerceContext>();
            colourContext.Setup(ctx => ctx.Colours).ReturnsDbSet(new List<Colour>() { colour });
            IColourRepository colourRepository = new ColourRepository(colourContext.Object);
            var expectedReturn = colourRepository.GetColours();
            Assert.IsTrue(expectedReturn.Contains(colour));
        }
    }
}