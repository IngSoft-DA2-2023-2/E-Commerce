using DataAccess;
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
        public void CreateGenreOk()
        {
            Product product = new Product() {Name = "Sample"};
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var expectedReturn = productRepository.CreateProduct(product);
            Assert.AreEqual(expectedReturn, product);
        }
    }
}