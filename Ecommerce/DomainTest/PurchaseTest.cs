using Domain;
using System.Diagnostics.CodeAnalysis;

namespace DomainTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PurchaseTest
    {
        [TestMethod]
        public void SetPurchasePromotionNull()
        {
            Purchase purchase = new Purchase()
            {
                CurrentPromotion = "Promotion20Off",
            };
            Assert.AreEqual(purchase.CurrentPromotion, "Promotion20Off");
            purchase.DropPromotion();
            Assert.AreEqual(purchase.CurrentPromotion, null);
        }
    }
}
