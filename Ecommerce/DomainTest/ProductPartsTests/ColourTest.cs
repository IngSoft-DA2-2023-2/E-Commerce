using Domain.ProductParts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
