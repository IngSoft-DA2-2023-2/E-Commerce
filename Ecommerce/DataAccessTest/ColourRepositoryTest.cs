using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ColourRepositoryTest
    {
        [TestMethod]
        public void GivenExistingColourNameReturnsTrue()
        {
            Colour colour = new Colour() { Name = "colour" };
            string colourName = "colour";
            var colourContext = new Mock<ECommerceContext>();
            colourContext.Setup(ctx => ctx.Colours).ReturnsDbSet(new List<Colour>() { colour });
            IColourRepository colourRepository = new ColourRepository(colourContext.Object);
            var expectedReturn = colourRepository.CheckForColour(colourName);
            Assert.IsTrue(expectedReturn);
        }
        [TestMethod]
        public void GivenNonExistingColourNameThrowsException()
        {
            string colourName = "colour";
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
            Assert.IsTrue(catchedException.Message.Equals("Colour does not exists"));
        }
    }
}
