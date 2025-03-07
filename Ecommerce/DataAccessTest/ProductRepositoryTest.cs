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
            Product product = new Product()
            {
                Name = "Sample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour>() { new Colour() { Name = "Red" } },
                Description = "Description",
                Price = 10
            };
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
        public void UpdateProductStockOk()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid(), Stock = 5 };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            int response = productRepository.UpdateStock(product);
            Assert.AreEqual(response, 4);
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
            productContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByName(product.Name);
            Assert.AreEqual(response.First().Name, product.Name);
        }

        [TestMethod]
        public void GetEmptyListAfterFilteringProductsByName()
        {
            Product product = new Product() { Name = "Sample", Id = new Guid() };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);

            var ret = productRepository.GetProductByName(product.Name);

            Assert.AreEqual(ret.Count(), 0);
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
            productContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByBrand("brand");
            Assert.AreEqual(response.First().Brand.Name, "brand");
        }

        [TestMethod]
        public void GetEmptyListAfterFilteringProductsByBrand()
        {
            Product product = new()
            {
                Name = "Sample",
                Brand = new Brand() { Name = "brand" },
                Id = new Guid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);

            var ret = productRepository.GetProductByBrand("brand");

            Assert.AreEqual(ret.Count(), 0);
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
            productContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByCategory("category");
            Assert.AreEqual(response.First().Category.Name, "category");
        }

        [TestMethod]
        public void ReturnsEmptyListWhenTryingToGetProductByCategoryThatHasNoElements()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Category =
                new Category() { Name = "category" },
                Id = Guid.NewGuid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);

            var ret = productRepository.GetProductByCategory(product.Category.Name);

            Assert.AreEqual(ret.Count(), 0);
        }

        [TestMethod]
        public void ReturnsEmptyListWhenTryingToGetProductByPriceRange()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Price = 20,
                Id = new Guid()
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByPriceRange("0-10");
            Assert.AreEqual(response.Count(), 0);
        }

        [TestMethod]
        public void GetFilteredProductByPriceRange()
        {
            Product product = new Product()
            {
                Name = "Sample",
                Price = 20,
                Id = new Guid(),
                Stock = 1,
            };
            var productContext = new Mock<ECommerceContext>();
            productContext.Setup(ctx => ctx.Products).ReturnsDbSet(new List<Product>() { product });
            productContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IProductRepository productRepository = new ProductRepository(productContext.Object);
            var response = productRepository.GetProductByPriceRange("0-100");
            Assert.AreEqual(response.First().Price, 20);
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
            productContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
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