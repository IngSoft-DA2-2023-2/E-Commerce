using BusinessLogic;
using DataAccessInterface;
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
    public class CategoryLogicTest
    {
        [TestMethod]
        public void GivenExistingCategoryReturnsTrue()
        {
            Category expected = new()
            {
                Name = "Category"
            };

            Mock<ICategoryRepository> repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForCategory("Category")).Returns(true);
            var categoryLogic = new CategoryLogic(repository.Object);
            var result = categoryLogic.CheckForCategory(expected);
            repository.VerifyAll();
            Assert.IsTrue(result);
        }

    }
}
