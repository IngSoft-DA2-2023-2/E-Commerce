using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PurchaseRepositoryTest
    {
        [TestMethod]
        public void CreatePurchaseOk()
        {
            Purchase purchase = new Purchase() { Id = Guid.NewGuid() };
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            var expectedReturn = purchaseRepository.CreatePurchase(purchase);
            Assert.AreEqual(expectedReturn, purchase);
        }
        [TestMethod]
        public void CreateAlreadyExistingPurchase()
        {
            Purchase purchase = new Purchase() { Id = Guid.NewGuid() };
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            Exception catchedException = null;
            try
            {
                purchaseRepository.CreatePurchase(purchase);
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals($"Purchase already exists."));
        }
        [TestMethod]
        public void GetAllTheBuyersPurchases()
        {
            Guid buyer = Guid.NewGuid();
            Purchase purchase = new Purchase()
            {
                Id = Guid.NewGuid(),
                UserId = buyer,
                Cart = new List<Product>
                {
                    new Product
                    {
                        Name="producto",
                        Id =Guid.NewGuid(),
                        Price = 100,
                        Description = "descripcion",
                    }
                }
            };
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            var expectedReturn = purchaseRepository.GetPurchase(buyer);
            Assert.AreEqual(expectedReturn.First(), purchase);
        }
        [TestMethod]
        public void GetAllPurchases()
        {
            Purchase purchase = new Purchase() { Id = Guid.NewGuid() };
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            var expectedReturn = purchaseRepository.GetAllPurchases();
            Assert.AreEqual(expectedReturn.First(), purchase);
        }
        [TestMethod]
        public void ThrowExceptionWhenNullList()
        {
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            Exception catchedException = null;
            try
            {
                purchaseRepository.GetAllPurchases();
            }
            catch (Exception ex)
            {
                catchedException = ex;
            };
            Assert.IsInstanceOfType(catchedException, typeof(DataAccessException));
            Assert.IsTrue(catchedException.Message.Equals("List is null"));
        }
    }
}
