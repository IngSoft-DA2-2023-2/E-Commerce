﻿using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class CategoryRepositoryTest
    {
        [TestMethod]
        public void GivenExistingCategoryNameReturnsTrue()
        {
            Category category = new Category() { Name = "category" };
            string categoryName = "category";
            var categoryContext = new Mock<ECommerceContext>();
            categoryContext.Setup(ctx => ctx.Categories).ReturnsDbSet(new List<Category>() { category });
            ICategoryRepository categoryRepository = new CategoryRepository(categoryContext.Object);
            var expectedReturn = categoryRepository.CheckForCategory(categoryName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingCategoryNameThrowsException()
        {
            string categoryName = "category";
            var categoryContext = new Mock<ECommerceContext>();
            categoryContext.Setup(ctx => ctx.Categories).ReturnsDbSet(new List<Category>() { });
            ICategoryRepository categoryRepository = new CategoryRepository(categoryContext.Object);
            Exception catchedException = null;
            try
            {
                categoryRepository.CheckForCategory(categoryName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"Category {categoryName} does not exists"));
            
          
        }

    }
}