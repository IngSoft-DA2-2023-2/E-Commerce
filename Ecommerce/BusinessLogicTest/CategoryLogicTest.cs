using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
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
            var result = categoryLogic.CheckForCategory(expected.Name);
            repository.VerifyAll();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivennonExistingCategoryThrowsException()
        {
            Category expected = new()
            {
                Name = "Category"
            };

            Mock<ICategoryRepository> repository = new Mock<ICategoryRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CheckForCategory("Category")).Throws(new DataAccessException("Category Category does not exists"));
            var categoryLogic = new CategoryLogic(repository.Object);
            Exception catchedException = null;
            try
            {
                categoryLogic.CheckForCategory(expected.Name);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.IsTrue(catchedException?.Message.Equals("Category Category does not exists"));
        }

        [TestMethod]
        public void GivenExistingCategoryReturnsCategory()
        {
            Category category = new Category() { Name = "category" };
            var categoryText = new Mock<ICategoryRepository>();
            categoryText.Setup(ctx => ctx.GetCategories()).Returns(new List<Category>() { category });
            ICategoryLogic categoryLogic = new CategoryLogic(categoryText.Object);
            var expectedReturn = categoryLogic.GetCategories();
            Assert.IsTrue(expectedReturn.Contains(category.Name));
        }

    }
}
