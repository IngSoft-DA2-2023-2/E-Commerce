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
    public class CategoryTest
    {
        [TestMethod]
        public void GetCategoryId()
        {
            Guid guid = Guid.NewGuid();
            Category category = new Category()
            {
                Id = guid,
            };
            Assert.AreEqual(guid, category.Id);
        }

        [TestMethod]
        public void CategoryEqualsReturnsTrue()
        {
            string name = "category";
            Category category1 = new Category()
            {
                Name = name,
            };
            Category category2 = new Category()
            {
                Name = name,
            };
            Assert.IsTrue(category1.Equals(category2));
        }
    }
}
