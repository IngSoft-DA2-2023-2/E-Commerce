using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
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
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase});
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
            Assert.IsTrue(catchedException.Message.Equals($"Purchase {purchase.Id} already exists."));
        }
        [TestMethod]
        public void GetAllTheBuyersPurchases()
        {
            Guid buyer = Guid.NewGuid();
            Purchase purchase = new Purchase() { Id = Guid.NewGuid(),BuyerId = buyer };
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            var expectedReturn = purchaseRepository.GetPurchases(buyer);
            Assert.AreEqual(expectedReturn.First(), purchase);
        }
        [TestMethod]
        public void GetAllPurchases()
        {
            Purchase purchase = new Purchase() { Id = Guid.NewGuid()};
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() { purchase });
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            var expectedReturn = purchaseRepository.GetPurchases(null);
            Assert.AreEqual(expectedReturn.First(), purchase);
        }
        [TestMethod]
        public void ThrowExceptionWhenNullList()
        {
            var purchaseContext = new Mock<ECommerceContext>();
            purchaseContext.Setup(ctx => ctx.Purchases).ReturnsDbSet(new List<Purchase>() {});
            IPurchaseRepository purchaseRepository = new PurchaseRepository(purchaseContext.Object);
            Exception catchedException = null;
            try
            {
                purchaseRepository.GetPurchases(null);
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
