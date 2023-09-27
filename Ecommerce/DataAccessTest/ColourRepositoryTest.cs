using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
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
    }
}
