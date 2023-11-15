using BusinessLogic;
using DataAccessInterface;
using Domain.ProductParts;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringWrapperLogicTest
    {
        [TestMethod]
        public void GetAllRoles()
        {

            StringWrapper stringWrapper = new StringWrapper()
            {
                Id = Guid.NewGuid(),
                Info = "admin"
            };

            List<StringWrapper> stringWrappers = new List<StringWrapper>() { stringWrapper };
            Mock<IStringWrapperRepository> stringWrapperRepository = new Mock<IStringWrapperRepository>();
            stringWrapperRepository.Setup(p => p.GetRoles()).Returns(stringWrappers);
            StringWrapperLogic stringWrapperLogic = new StringWrapperLogic(stringWrapperRepository.Object);
            var expected = stringWrapperLogic.GetRoles();
            Assert.AreEqual(expected, stringWrappers);
        }
    }
}