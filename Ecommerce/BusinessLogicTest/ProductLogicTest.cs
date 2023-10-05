using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using LogicInterface.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class ProductLogicTest
    {
        [TestMethod]
        public void CreateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour" } }
            };

            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.CreateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            var result = productLogic.AddProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name, expected.Name);
        }

        [TestMethod]
        public void UpdateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour> { new Colour { Name = "Colour" } }
            };

            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.UpdateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            var result = productLogic.UpdateProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name, expected.Name);
        }

        [TestMethod]
        public void FilterProductUnionOk()
        {
            Guid id = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            Guid id3 = Guid.NewGuid();
            Product expected1 = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "description1",
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };
            Product expected2 = new()
            {
                Id = id2,
                Name = "ProductSample2",
                Description = "description2",
                Brand = new Brand() { Name = "Brand2" },
                Category = new Category() { Name = "Category2" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour2" } }
            };
            Product expected3 = new()
            {
                Id = id3,
                Name = "ProductSample3",
                Description = "description3",
                Brand = new Brand() { Name = "Brand3" },
                Category = new Category() { Name = "Category3" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour3" } }
            };
            IEnumerable<Product> list = new List<Product>() { expected1, expected2, expected3 };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>();
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample1")).Returns(new List<Product>() { expected1 });
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand2")).Returns(new List<Product>() { expected2 });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category3")).Returns(new List<Product>() { expected3 });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterUnionProduct("ProductSample1", "Brand2", "Category3");
            productRepo.VerifyAll();
            Assert.IsTrue(result.Contains(expected1));
        }

        [TestMethod]
        public void FilterProductIntersectionOk()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price =10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample1")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand2")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category3")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct("ProductSample1", "Brand2", "Category3");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }
        [TestMethod]
        public void FilterProductIntersectionThrowsException()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample1")).Throws(new DataAccessException("Product ProductSample1 does not exist."));
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            Exception catchedException = null;
            try
            {
                productLogic.FilterIntersectionProduct("ProductSample1", "Brand2", "Category3");
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.AreEqual(catchedException?.Message, "Product ProductSample1 does not exist.");
        }
        [TestMethod]
        public void GivenExistingProductReturnsTrue()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetAllProducts()).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            bool result = productLogic.CheckProduct(expected);
            productRepo.VerifyAll();
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void GivenNonExistingProductThrowsException()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetAllProducts()).Returns(new List<Product>() { });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            Exception catchedException = null;
            try
            {
                productLogic.CheckProduct(expected);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.AreEqual(catchedException?.Message, "Product Does not exists.");

        }
        [TestMethod]
        public void GetProductById()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(l => l.GetProductById(id)).Returns(expected);
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.GetProductById(id);
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void ThrowExceptionTryingToGetProductById()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
                Description = "Description",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(l => l.GetProductById(id)).Throws(new DataAccessException($"Product with id {id} does not exist."));
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            Exception catchedException = null;
            try
            {
                productLogic.GetProductById(id);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.IsTrue(catchedException?.Message.Equals($"Product with id {id} does not exist."));
        }

    }
}
