using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringWrapperRepositoryTest
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
            var rolesContext = new Mock<ECommerceContext>();
            rolesContext.Setup(ctx => ctx.StringListWrappers).ReturnsDbSet(stringWrappers);
            IStringWrapperRepository stringWrapperRepository = new StringWrapperRepository(rolesContext.Object);
            var expected = stringWrapperRepository.GetRoles();
            Assert.IsTrue(expected.Contains(stringWrapper));

        }
    }
}