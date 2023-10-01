using BusinessLogic;
using DataAccessInterface;
using Domain;
using Domain.ProductParts;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class ProductLogicTest
    {
        [TestMethod]
        public void CreateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand"},
                Category = new Category() { Name = "Category"},
                Colours = new List<Colour>() {new Colour() {Name = "Colour" } }
            };

            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            Mock<IBrandRepository> brandRepo = new Mock<IBrandRepository>(MockBehavior.Strict);
            Mock<ICategoryRepository> categoryRepo = new Mock<ICategoryRepository>(MockBehavior.Strict);
            Mock<IColourRepository> colourRepo = new Mock<IColourRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.CreateProduct(It.IsAny<Product>())).Returns(expected);
            brandRepo.Setup(bLogic => bLogic.CheckForBrand("Brand")).Returns(true);
            categoryRepo.Setup(CaLogic => CaLogic.CheckForCategory("Category")).Returns(true);
            colourRepo.Setup(CoLogic => CoLogic.CheckForColour("Colour")).Returns(true);
            var productLogic = new ProductLogic(productRepo.Object,brandRepo.Object,categoryRepo.Object,colourRepo.Object);
            var result = productLogic.AddProduct(expected);
            productRepo.VerifyAll();
            brandRepo.VerifyAll();
            categoryRepo.VerifyAll();
            colourRepo.VerifyAll();
            Assert.AreEqual(result.Name,expected.Name);
        }

        [TestMethod]
        public void UpdateProductCorrectly()
        {
            Product expected = new()
            {
                Name = "ProductSample",
                Brand = new Brand() { Name = "Brand" },
                Category = new Category() { Name = "Category" },
                Colours = new List<Colour> { new Colour {Name= "Colour" } }
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
            Guid id= Guid.NewGuid();
            Product expected1 = new()
            {
                Id= id,
                Name = "ProductSample1",
                Brand = new Brand() { Name = "Brand1" },
                Category = new Category() { Name = "Category1" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour1" } }
            };
            Product expected2 = new()
            {
                Id = id,
                Name = "ProductSample2",
                Brand = new Brand() { Name = "Brand2" },
                Category = new Category() { Name = "Category2" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour2" } }
            };
            Product expected3 = new()
            {
                Id = id,
                Name = "ProductSample3",
                Brand = new Brand() { Name = "Brand3" },
                Category = new Category() { Name = "Category3" },
                Colours = new List<Colour>() { new Colour() { Name = "Colour3" } }
            };
            IEnumerable<Product> list = new List<Product>() { expected1,expected2, expected3 };
            Mock<IProductRepository> productRepo = new Mock<IProductRepository>(MockBehavior.Strict);
            productRepo.Setup(pLogic => pLogic.GetProductByName("ProductSample1")).Returns(new List<Product>() { expected1});
            productRepo.Setup(pLogic => pLogic.GetProductByBrand("Brand2")).Returns(new List<Product>() { expected2 });
            productRepo.Setup(pLogic => pLogic.GetProductByCategory("Category3")).Returns(new List<Product>() { expected3 });
            var productLogic = new ProductLogic(productRepo.Object,null, null, null);
            var result = productLogic.FilterUnionProduct("ProductSample1", "Brand2", "Category3");
            productRepo.VerifyAll();
            Assert.IsTrue(result.All(x=> list.Contains(x)));
        }

        [TestMethod]
        public void FilterProductIntersectionOk()
        {
            Guid id = Guid.NewGuid();
            Product expected = new()
            {
                Id = id,
                Name = "ProductSample1",
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
            var result = productLogic.FilterUnionProduct("ProductSample1", "Brand2", "Category3");
            productRepo.VerifyAll();
            Assert.AreEqual(expected,result.First());
        }

    }
}
