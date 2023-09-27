using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
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
    }
}
