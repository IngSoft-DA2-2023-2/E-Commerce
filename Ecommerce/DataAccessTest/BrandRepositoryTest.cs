using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class BrandRepositoryTest
    {
        [TestMethod]
        public void GivenExistingBrandNameReturnsTrue()
        {
            Brand brand = new Brand() { Name = "brand" };
            string brandName = "brand";
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() { brand });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            var expectedReturn = brandRepository.CheckForBrand(brandName);
            Assert.IsTrue(expectedReturn);
        }

        [TestMethod]
        public void GivenNonExistingBrandNameThrowsException()
        {
            string brandName = "brand";
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() { });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            Exception catchedException = null;
            try
            {
                brandRepository.CheckForBrand(brandName);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"Brand {brandName} does not exists."));


        }

        [TestMethod]
        public void GivenExistingBrandReturnsBrands()
        {
            Brand brand = new Brand() { Name = "brand" };
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() { brand });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            var expectedReturn = brandRepository.GetBrands();
            Assert.IsTrue(expectedReturn.Contains(brand));
        }

        [TestMethod]
        public void GivenNonExistingBrandReturnsEmptyList()
        {
            var brandContext = new Mock<ECommerceContext>();
            brandContext.Setup(ctx => ctx.Brands).ReturnsDbSet(new List<Brand>() { });
            IBrandRepository brandRepository = new BrandRepository(brandContext.Object);
            var expectedReturn = brandRepository.GetBrands();
            Assert.IsTrue(expectedReturn.Count() == 0);
        }
    }
}
