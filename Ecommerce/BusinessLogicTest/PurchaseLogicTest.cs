using BusinessLogic;
using DataAccessInterface;
using Domain;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class PurchaseLogicTest
    {

        [TestMethod]
        public void CreatePurchaseCorrectly()
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
            Mock<IPurchaseRepository> repository = new Mock<IPurchaseRepository>(MockBehavior.Strict);
            repository.Setup(logic => logic.CreatePurchase(It.IsAny<Purchase>())).Returns(purchase);
            var purchaseLogic = new PurchaseLogic(repository.Object);
            var result = purchaseLogic.CreatePurchase(purchase);
            repository.VerifyAll();
            Assert.AreEqual(result.Cart.First().Name, purchase.Cart.First().Name);
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
            repository.Setup(logic => logic.GetPurchases(null)).Returns(purchaseList);
            var purchaseLogic = new PurchaseLogic(repository.Object);
            var result = purchaseLogic.GetPurchases(null);
            repository.VerifyAll();
            Assert.AreEqual(result.First(), purchase);
        }
    }
}
