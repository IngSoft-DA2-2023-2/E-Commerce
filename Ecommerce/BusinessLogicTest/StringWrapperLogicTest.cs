using BusinessLogic;
using DataAccessInterface;
using Domain.ProductParts;
using LogicInterface;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Info = "Admin"
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
