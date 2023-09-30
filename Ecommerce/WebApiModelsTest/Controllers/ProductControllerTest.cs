using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Moq;
using System;
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

            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>(){new StringWrapper(){Info="admin"} },
                    Id = guid
                },
            };


            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("or",null,null,null) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void GetAllProductsEmpty()
        {
            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };


            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            List<Product> products = new List<Product>();
            Mock<IProductLogic> mock = new Mock<IProductLogic>();
            mock.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(mock.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("or",null,null,null) as OkObjectResult;
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
            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };


            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == exceptedName),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("or",exceptedName,null,null) as OkObjectResult;
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

            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };


            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == exceptedBrandName),
                It.Is<string?>(categoryName => categoryName == null))).Returns(products);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("or",null,exceptedBrandName,null) as OkObjectResult;
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

            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };


            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.FilterUnionProduct(It.Is<string?>(name => name == null),
                It.Is<string?>(brandName => brandName == null),
                It.Is<string?>(categoryName => categoryName == exceptedCategoryName))).Returns(products);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("or",null,null, exceptedCategoryName) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(products, result.Value);
        }


        [TestMethod]
        public void GetAllProductsWithSpecificNameAndCategory()
        {
            const string exceptedCategoryName = "Category1";
            List<Product> products = new List<Product>()
            {
                product,
            };

            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsBuyer(It.Is<string>(s => s == guid.ToString()))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.FilterIntersectionProduct(It.IsAny<string?>(),It.IsAny<string?>(),It.IsAny<string?>())).Returns(products);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetAllProductsByFilters("and", "name1", null, exceptedCategoryName) as OkObjectResult;
            Assert.AreEqual(products, result.Value);
        }

        [TestMethod]
        public void CreateNewProduct()
        {
            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List < StringWrapper > { new StringWrapper() { Info = "admin" } },
                    Id = guid
                },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.AddProduct(It.Is<Product>(product =>
              product.Name == productRequest.Name &&
              product.Description == productRequest.Description &&
              product.Category.Name == productRequest.Category &&
              product.Brand.Name == productRequest.Brand &&
              product.Colors.First().Name == productRequest.Color.First() &&
              product.Price == productRequest.Price))).Returns(product);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.CreateProduct(productRequest, token) as OkObjectResult;
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
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List < StringWrapper > { new StringWrapper() { Info = "admin" } },
                    Id = Guid.NewGuid()
                    },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);


            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.AddProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Throws(new TestException("This is a test exception"));
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            Assert.ThrowsException<TestException>(() => productController.CreateProduct(productRequest, token));
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
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper() { Info = "admin" } },
                    Id = Guid.NewGuid()
                    },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.UpdateProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Returns(product); ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            Guid id = new Guid();
            var result = productController.UpdateProduct(id, productRequest, token) as OkObjectResult;
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

            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper() { Info = "admin" } },
                    Id = Guid.NewGuid()
                    },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);

            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.UpdateProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Throws(new TestException("This is a test exception"));
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);

            Assert.ThrowsException<TestException>(() => productController.UpdateProduct(guid, productRequest, token));

        }

        [TestMethod]
        public void GetProductByIdOk()
        {
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{new StringWrapper() { Info = "admin" } },
                    Id = Guid.NewGuid()
                    },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);

            Guid guid = Guid.NewGuid();
            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.GetProductById(It.Is<Guid>(id => id == guid))).Returns(product);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            var result = productController.GetProductById(guid) as OkObjectResult;
            Assert.IsNotNull(result);
            Assert.AreEqual(product, result.Value);
        }

        [TestMethod]
        public void UpdateProductUnauthorized()
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
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper { Id = new Guid(), Info =  "buyer" } },
                    Id = new Guid()
                    },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(false);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.UpdateProduct(It.Is<Product>(product =>
            product.Name == productRequest.Name &&
            product.Description == productRequest.Description &&
            product.Category.Name == productRequest.Category &&
            product.Brand.Name == productRequest.Brand &&
            product.Colors.First().Name == productRequest.Color.First() &&
            product.Price == productRequest.Price))).Returns(product); ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            Guid id = new Guid();
            Assert.ThrowsException<UnauthorizedAccessException>(() => productController.UpdateProduct(id, productRequest, token));
        }

        [TestMethod]
        public void CreateProductUnauthorized()
        {
            Guid guid = Guid.NewGuid();
            IEnumerable<User> listUsers = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<StringWrapper>{ new StringWrapper { Id = new Guid(), Info =  "buyer" } },
                    Id = guid
                },
            };

            string token = "tokenSample";
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(listUsers);
            userLogic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(false);

            Mock<IProductLogic> productLogic = new Mock<IProductLogic>();
            productLogic.Setup(p => p.AddProduct(It.Is<Product>(product =>
              product.Name == productRequest.Name &&
              product.Description == productRequest.Description &&
              product.Category.Name == productRequest.Category &&
              product.Brand.Name == productRequest.Brand &&
              product.Colors.First().Name == productRequest.Color.First() &&
              product.Price == productRequest.Price))).Returns(product);
            ProductController productController = new ProductController(productLogic.Object, userLogic.Object);
            Assert.ThrowsException<UnauthorizedAccessException>(() => productController.CreateProduct(productRequest, token));

        }





    }
}
