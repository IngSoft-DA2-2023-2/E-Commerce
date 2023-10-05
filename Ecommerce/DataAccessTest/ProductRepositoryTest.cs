using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
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
            Assert.AreEqual(catchedException?.Message, "Product Sample already exists.");
        }

        [TestMethod]
        public void UpdateProductOk()
        {
            Guid id = Guid.NewGuid();
            Product product = new Product()
            {
                Name = "Sample",
                Id = id,
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour>() { new Colour() { Name = "Red" } }
            };

            var listProducts = new List<Product>() { product };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(listProducts);

            Product expectedReturn = new Product()
            {
                Name = "Sample2",
                Id = id,
                Brand = new Brand() { Name = "Brand2" },
                Category = new Category() { Name = "Category2" },
                Colours = new List<Colour>() { new Colour() { Name = "Green" } }
            };
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var result = productRepository.UpdateProduct(expectedReturn);
            Assert.AreEqual(expectedReturn, result);
        }


        [TestMethod]
        public void UpdateNonExistingProduct()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            var expectedReturn = new Product()
            {
                Name = "Sample",
                Id = new Guid(),
                Colours = new List<Colour>() { new Colour() { Name = "Green" } }
            };
        }

        [TestMethod]
        public void GetProductByIdOk()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductById(product.Id);
            Assert.AreEqual(response, product);
        }
        [TestMethod]
        public void ThrowExceptionTryingToGetProductById()
        {
            Product product = new Product() { Name = "Sample", Id = Guid.NewGuid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            Exception catchedException = null;
            try
            {
                productRepository.GetProductById(product.Id);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.AreEqual(catchedException?.Message, $"Product with id {product.Id} does not exist.");
        }
        [TestMethod]
        public void GetFilteredProductByName()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByName(product.Name);
            Assert.AreEqual(response.First().Name, product.Name);
        }
        [TestMethod]
        public void GetExceptionAfterFilteringProductsByName()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            Exception catchedException = null;
            try
            {
                productRepository.GetProductByName(product.Name);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.AreEqual(catchedException?.Message, $"Product {product.Name} does not exist.");
        }
        [TestMethod]
        public void GetFilteredProductByBrand()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Brand = new Brand() { Name = "brand" },
                Id = new Guid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByBrand("brand");
            Assert.AreEqual(response.First().Brand.Name, "brand");
        }
        [TestMethod]
        public void GetExceptionAfterFilteringProductsByBrand()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Brand = new Brand() { Name = "brand" },
                Id = new Guid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            Exception catchedException = null;
            try
            {
                productRepository.GetProductByBrand("brand");
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.AreEqual(catchedException?.Message, "Product brand brand does not exist.");
        }
        [TestMethod]
        public void GetFilteredProductByCategory()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Category = new Category() { Name = "category" },
                Id = new Guid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByCategory("category");
            Assert.AreEqual(response.First().Category.Name, "category");
        }
        [TestMethod]
        public void ThrowExceptionTryingToGetProductByCategory()
        {
            Product product = new Product() { Name = "Sample",Category= new Category() { Name = "category" }, Id = Guid.NewGuid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            Exception catchedException = null;
            try
            {
                productRepository.GetProductByCategory(product.Category.Name);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.AreEqual(catchedException?.Message, "Product category category does not exist.");
        }

        [TestMethod]
        public void GetAllProducts()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Brand = new Brand() { Name = "brand" },
                Id = Guid.NewGuid()
            };
            var products = new List<Product>() { product };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(products);
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetAllProducts();
            Assert.AreEqual(response.Count(), products.Count());
            for (int i = 0; i < response.Count(); i++)
            {
                Assert.AreEqual(response.ElementAt(i), products.ElementAt(i));
            }
        }

    }
}
