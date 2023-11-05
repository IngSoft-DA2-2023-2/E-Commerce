using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface.Exceptions;
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
        [TestMethod]
        public void GivennonExistingBrandThrowsException()
        {
            Brand expected = new()
            {
                Name = "Brand"
            };

            Mock<IBrandRepository> repository = new Mock<IBrandRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForBrand("Brand")).Throws(new DataAccessException("Brand Brand does not exists"));
            var brandLogic = new BrandLogic(repository.Object);
            Exception catchedException = null;
            try
            {
                brandLogic.CheckBrand(expected);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.IsTrue(catchedException?.Message.Equals("Brand Brand does not exists"));
        }

    }
}
