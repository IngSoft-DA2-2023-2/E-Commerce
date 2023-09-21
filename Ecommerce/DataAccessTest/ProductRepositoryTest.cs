using DataAccess.Context;
using DataAccess.Exceptions;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;

namespace DataAccessTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void CreateProductOk()
        {
            Product product = new Product() { Name = "Sample" };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var expectedReturn = productRepository.CreateProduct(product);
            Assert.AreEqual(expectedReturn, product);
        }
        [TestMethod]
        public void CreateAlreadyExistingProduct()
        {
            Product product = new Product() { Name = "Sample" };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            Exception catchedException = null;
            try
            {
                productRepository.CreateProduct(product);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals("Product Sample already exists."));
        }
    }
}