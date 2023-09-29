using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Moq;
using WebApi.Controllers;
using WebApiTest.Exceptions;

namespace WebApiModelsTest.Controller
{

    [TestClass]
    public class ProductControllerTest
    {
        List<string> stringColor;
        List<Colour> color;
        CreateProductRequest productRequest;
        Product product;
        Product secondProduct;

        [TestInitialize]
        public void Init()
        {
            stringColor = new List<string>() { "Red", "Blue" };
            color = new List<Colour>() { new Colour() { Name = "Red"}, new Colour() { Name = "Blue" } };

             productRequest = new CreateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = stringColor,
                Price = 100
            };
             product = new Product()
            {
                Name = "Name1",
                Description = "Description1",
                Category = new Category() { Name = "Category1" },
                Brand = new Brand() { Name = "Brand1" },
                Colors = color,
                Price = 100
            };
            secondProduct = new Product()
            {
                Name = "Name2",
                Description = "Description2",
                Category = new Category() { Name = "Category2" },
                Brand = new Brand() { Name = "Brand2" },
                Colors = color,
                Price = 100
            };
        }

        [TestMethod]
        public void GetAllProductsOk()
        {
            List<Product> products = new List<Product>
            {
                product,
                secondProduct
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsInternalServerError()
        {
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Throws(new TestException("This is a test exception"));
            ProductController productController = new ProductController(mock.Object);
            Assert.ThrowsException<TestException>(() => productController.GetAllProductsByFilters());
        }

        [TestMethod]
        public void GetAllProductsEmpty()
        {
            List<Product> products = new List<Product>();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters() as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithNameOk()
        {
            const string exceptedName = "Name1";
            List<Product> products = new List<Product>
            {
                product,
                secondProduct
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == exceptedName),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(name: exceptedName) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithBrandNameOk()
        {
            const string exceptedBrandName = "Brand1";
            List<Product> products = new List<Product>
            {
                product,
                secondProduct
            };
            
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == exceptedBrandName),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(brandName: exceptedBrandName) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsWithCategoryNameOk()
        {
            const string exceptedCategoryName = "Category1";
            List<Product> products = new List<Product>()
            {
                product,
                secondProduct
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == exceptedCategoryName))).Returns(products);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetAllProductsByFilters(categoryName: exceptedCategoryName) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }


        [TestMethod]
        public void CreateNewProduct()
        {  
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.AddProduct(It.Is<Product>(product => 
              product.Name == productRequest.Name &&
              product.Description == productRequest.Description &&
              product.Category.Name == productRequest.Category &&
              product.Brand.Name == productRequest.Brand && 
              product.Colors.First().Name == productRequest.Color.First() &&
              product.Price == productRequest.Price))).Returns(product);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.CreateProduct(productRequest) as OkObjectResult;          
            Assert.IsNotNull(result);
            var response = result.Value as CreateProductResponse;
            Assert.AreEqual(productRequest.Name, response.Name);
            Assert.AreEqual(productRequest.Description, response.Description);
            Assert.AreEqual(productRequest.Category, response.Category);
            Assert.AreEqual(productRequest.Brand, response.Brand);
            Assert.AreEqual(productRequest.Color.First(), response.Colors.First());
            Assert.AreEqual(productRequest.Price, response.Price);
        }

        [TestMethod]
        public void CreateNewProductInternalServerError()
        {
            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.AddProduct(It.Is<Product>(product => 
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Throws(new TestException("This is a test exception"));
            ProductController productController = new ProductController(mock.Object);
            Assert.ThrowsException<TestException>(() => productController.CreateProduct(productRequest));
        }


        [TestMethod]
        public void UpdateNewProduct()
        {
            List<string> color = new List<string>() { "Red", "Blue" };
            UpdateProductRequest productRequest = new UpdateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = color,
                Price = 100
            };
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.UpdateProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Returns(product); ProductController productController = new ProductController(mock.Object);
            Guid id = new Guid();
            var result = productController.UpdateProduct(id,productRequest) as OkObjectResult;
            Assert.IsNotNull(result);
            var response = result.Value as UpdateProductResponse;
            Assert.AreEqual(productRequest.Name, response.Name);
            Assert.AreEqual(productRequest.Description, response.Description);
            Assert.AreEqual(productRequest.Category, response.Category);
            Assert.AreEqual(productRequest.Brand, response.Brand);
            Assert.AreEqual(productRequest.Color.First(), response.Colors.First());
            Assert.AreEqual(productRequest.Price, response.Price);
        }

        [TestMethod]
        public void UpdateNewProductInternalServerError()
        {
            UpdateProductRequest productRequest = new UpdateProductRequest()
            {
                Name = "Name1",
                Description = "Description1",
                Category = "Category1",
                Brand = "Brand1",
                Color = new List<string>() { "Red", "Blue" },
                Price = 100
            };
            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.UpdateProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Throws(new TestException("This is a test exception"));
            ProductController productController = new ProductController(mock.Object);
            
            Assert.ThrowsException<TestException>(() => productController.UpdateProduct(guid, productRequest));

        }

        [TestMethod]
        public void GetProductByIdOk()
        {
            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.GetProductById(It.Is<Guid>(id => id == guid))).Returns(product);
            ProductController productController = new ProductController(mock.Object);
            var result = productController.GetProductById(guid) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.Value);
        }




    }
}
