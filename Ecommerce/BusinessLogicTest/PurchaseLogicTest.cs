using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.PaymentMethodCategories;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PurchaseLogicTest
    {
        [TestMethod]
        public void CreatePurchaseCorrectly()
        {
            Category category = new Category() { Name = "category" };
            Brand brand = new Brand() { Name = "brand" };
            Purchase purchase = new Purchase()
            {
                Cart = new List<Product>()
                {
                    new Product()
                    {
                    Name = "product1",
                    Description = "product1",
                    Category = category,
                    Brand= brand,
                    Price = 10,
                    },
                    new Product()
                    {
                    Name = "product2",
                    Description = "product2",
                    Category = category,
                    Brand= brand,
                    Price = 4,
                    },
                },
                PaymentMethod = new CreditCard()
                {
                    CategoryName = "CreditCard",
                    Flag = "Visa"
                }
            };
            Mock<IPurchaseRepository> repository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            Mock<IProductLogic> pLogic = new Mock<IProductLogic>();
            repository.Setup(logic => logic.CreatePurchase(It.IsAny<Purchase>())).Returns(purchase);
            pLogic.Setup(l => l.CheckProduct(It.IsAny<Product>())).Returns(true);
            var purchaseLogic = new PurchaseLogic(repository.Object, pLogic.Object);
            var result = purchaseLogic.CreatePurchase(purchase);
            repository.VerifyAll();
            Assert.AreEqual(result.Cart.First().Name, purchase.Cart.First().Name);
        }
        [TestMethod]
        public void ThrowsExceptionWhenTryingToCreatePurchase()
        {
            Guid guid = Guid.NewGuid();
            Purchase purchase = new Purchase()
            {
                Id = guid,
                Cart = new List<Product>()
                {
                    new Product
                    {
                          Name = "product2",
                          Description = "product2",
                          Brand = new Brand() { Name = "brand2" },
                          Category = new Category { Name = "category2"},
                          Price = 4,
                    }
                },
                PaymentMethod = new CreditCard()
                {
                    CategoryName = "CreditCard",
                    Flag = "Visa"
                }
            };
            Mock<IPurchaseRepository> repository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            Mock<IProductLogic> pLogic = new Mock<IProductLogic>();
            repository.Setup(logic => logic.CreatePurchase(It.IsAny<Purchase>())).Throws(new DataAccessException($"Purchase already exists."));
            pLogic.Setup(l => l.CheckProduct(It.IsAny<Product>())).Returns(true);
            var purchaseLogic = new PurchaseLogic(repository.Object, pLogic.Object);
            Exception catchedException = null;
            try
            {
                purchaseLogic.CreatePurchase(purchase);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            }
            Assert.IsInstanceOfType(catchedException, typeof(LogicException));
            Assert.AreEqual(catchedException?.Message, $"Purchase already exists.");
        }

        [TestMethod]
        public void GetAllPurchases()
        {
            Purchase purchase = new Purchase()
            {
                Cart = new List<Product>()
                {
                    new Product
                    {
                        Name = "Name",
                        Description = "Test",
                    }
                }
            };
            List<Purchase> purchaseList = new List<Purchase>() { purchase };
            Mock<IPurchaseRepository> repository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.GetAllPurchases()).Returns(purchaseList);
            var purchaseLogic = new PurchaseLogic(repository.Object, null);
            var result = purchaseLogic.GetAllPurchases();
            repository.VerifyAll();
            Assert.AreEqual(result.First(), purchase);
        }
        [TestMethod]
        public void GetPurchaseById()
        {
            Guid guid = Guid.NewGuid();
            Purchase purchase = new Purchase()
            {
                UserId = guid,
                Cart = new List<Product>()
                {
                    new Product
                    {
                        Name = "Name",
                        Description = "Test",
                    }
                }
            };
            List<Purchase> purchaseList = new List<Purchase>() { purchase };
            Mock<IPurchaseRepository> repository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.GetPurchase(guid)).Returns(purchaseList);
            var purchaseLogic = new PurchaseLogic(repository.Object, null);
            var result = purchaseLogic.GetPurchase(guid);
            repository.VerifyAll();
            Assert.AreEqual(result.First(), purchase);
        }
    }
}
