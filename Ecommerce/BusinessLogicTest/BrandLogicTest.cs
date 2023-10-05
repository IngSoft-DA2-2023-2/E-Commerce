using BusinessLogic;
using DataAccessInterface;
using Domain.ProductParts;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BrandLogicTest
    {
        [TestMethod]
        public void GivenExistingBrandReturnsTrue()
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
    }
}
