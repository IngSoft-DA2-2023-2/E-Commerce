using BusinessLogic;
using DataAccessInterface;
using Domain;
using Domain.ProductParts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class BrandLogicTest
    {
        [TestMethod]
        public void GivenExistingColorReturnsTrue()
        {
            Brand expected = new()
            {
                Name = "Brand"
            };

            Mock<IBrandRepository> repository = new Mock<IBrandRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForBrand("Brand")).Returns(true);
            var brandLogic = new BrandLogic(repository.Object);
            var result = brandLogic.CheckBrand(expected);
            repository.VerifyAll();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GivenNonExistingColorReturnsFalse()
        {
            Brand expected = new()
            {
                Name = "Brand"
            };

            Mock<IBrandRepository> repository = new Mock<IBrandRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForBrand("Brand")).Returns(false);
            var brandLogic = new BrandLogic(repository.Object);
            var result = brandLogic.CheckBrand(expected);
            repository.VerifyAll();
            Assert.IsFalse(result);
        }
    }
}
