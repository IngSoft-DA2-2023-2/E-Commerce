using Domain.ProductParts;
using System.Diagnostics.CodeAnalysis;

namespace DomainTest.ProductPartsTests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ColourTest
    {
        [TestMethod]
        public void GetColourId()
        {
            Guid guid = Guid.NewGuid();
            Colour colour = new Colour()
            {
                Id = guid,
            };
            Assert.AreEqual(guid, colour.Id);
        }
        [TestMethod]
        public void ColourEqualsReturnsTrue()
        {
            string name = "brand";
            Colour Colour1 = new Colour()
            {
                Name = name,
            };
            Colour Colour2 = new Colour()
            {
                Name = name,
            };
            Assert.IsTrue(Colour1.Equals(Colour2));
        }
    }
}
