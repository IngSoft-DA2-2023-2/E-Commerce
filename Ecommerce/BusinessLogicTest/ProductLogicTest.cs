using BusinessLogic;
using BusinessLogic.PaymentMethod;
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
        private Product expected;
        private Product expected1;
        private Product expected2;
        private Product expected3;
        private Product expected4;
        private Guid id1 = Guid.NewGuid();
        private Guid id2 = Guid.NewGuid();
        private Guid id3 = Guid.NewGuid();
        private Guid id4 = Guid.NewGuid();


        [TestInitialize]
        public void Init()
        {
            expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour" } },
                Description = "description",
                Price = 10
            };

            expected1 = new()
            {
                Id = id1,
                Name = "ProductSample1",
                Description = "description1",
                Price = 10,
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };

            expected2 = new()
            {
                Id = id2,
                Name = "ProductSample2",
                Description = "description2",
                Price = 10,
                Brand = new Brand() { Name = "Brand2" },
                Category = new Category() { Name = "Category2" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour2" } }
            };
            expected3 = new()
            {
                Id = id3,
                Name = "ProductSample3",
                Description = "description3",
                Price = 10,
                Brand = new Brand() { Name = "Brand3" },
                Category = new Category() { Name = "Category3" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour3" } }
            };

            expected4 = new()
            {
                Id = id1,
                Name = "ProductSample4",
                Description = "description4",
                Price = 50,
                Brand = new Brand() { Name = "Brand4" },
                Category = new Category() { Name = "Category4" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour4" } }
            };
        }

        [TestMethod]
        public void CreateProductCorrectly()
        {
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.CreateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new 
                ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            var result = productLogic.AddProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name, expected.Name);
        }
        [TestMethod]
        public void ThrowExceptionTryingToCreateProduct()
        {
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.CreateProduct(It.IsAny<Product>())).
                Throws(new DataAccessException("Product ProductSample already exists."));
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            Exception catchedException = null;
            try
            {
                productLogic.AddProduct(expected);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.AreEqual(catchedException?.Message, "Product ProductSample already exists.");
        }

        [TestMethod]
        public void UpdateProductCorrectly()
        {
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
        public void ThrowsExceptionTryingToUpdateProduct()
        {
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.UpdateProduct(It.IsAny<Product>())).
                Throws(new DataAccessException("Product does not exist."));
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new
                ProductLogic(productRepo.Object, brandRepo.Object, categoryRepo.Object, colourRepo.Object);
            Exception catchedException = null;
            try
            {
                productLogic.UpdateProduct(expected);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.IsTrue(catchedException?.Message.Equals("Product does not exist."));
        }

        [TestMethod]
        public void FilterProductUnionOk()
        {

            IEnumerable<Product> list = new List<Product>() { expected1, expected2, expected3 };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>();
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample1")).Returns(new List<Product>() { expected1 });
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand2")).Returns(new List<Product>() { expected2 });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category3")).Returns(new List<Product>() { expected3 });
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("50-100")).Returns(new List<Product>() { expected4 });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterUnionProduct("ProductSample1", "Brand2", "Category3", "50-100");
            productRepo.VerifyAll();
            Assert.IsTrue(result.Contains(expected1));
            Assert.IsTrue(result.Contains(expected2));
            Assert.IsTrue(result.Contains(expected3));
            Assert.IsTrue(result.Contains(expected4));
        }

        [TestMethod]
        public void FilterProductIntersectionOk()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;
            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("0-10")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct("ProductSample", "Brand", "Category", "0-10");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        public void FilterProductIntersectionWithoutNameOk()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;
            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("0-10")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct(null, "Brand", "Category", "0-10");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        public void FilterProductIntersectionWithoutBrandOk()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("0-10")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct("ProductSample", null, "Category", "0-10");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        public void FilterProductIntersectionWithoutNameAndBrandOk()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category")).Returns(new List<Product>() { expected });
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("0-10")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct(null, null, "Category", "0-10");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }

        [TestMethod]
        public void FilterProductIntersectionOnlyWithPriceOk()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByPriceRange("0-10")).Returns(new List<Product>() { expected });
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            var result = productLogic.FilterIntersectionProduct(null, null, null, "0-10");
            productRepo.VerifyAll();
            Assert.AreEqual(expected, result.First());
        }
        [TestMethod]
        public void FilterProductIntersectionThrowsException()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;

            IEnumerable<Product> list = new List<Product>() { expected };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample")).
                Throws(new DataAccessException("Product ProductSample does not exist."));
            var productLogic = new ProductLogic(productRepo.Object, null, null, null);
            Exception catchedException = null;
            try
            {
                productLogic.FilterIntersectionProduct("ProductSample", "Brand", "Category", "0-10");
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.AreEqual(catchedException?.Message, "Product ProductSample does not exist.");
        }

        [TestMethod]
        public void GivenExistingProductReturnsTrue()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;
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
            expected.Id = id;
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
            Assert.AreEqual(catchedException?.Message, "Product does not exists.");

        }

        [TestMethod]
        public void GetProductById()
        {
            Guid id = Guid.NewGuid();
            expected.Id = id;

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
            expected.Id = id;
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(l => l.GetProductById(id)).
                Throws(new DataAccessException($"Product with id {id} does not exist."));
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