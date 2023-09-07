using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion3x1FidelityTest
    {
        [TestMethod]
        public void GivenNotApplicablePurchaseReturnsFalse()
        {
            Purchase purchase = new Purchase()
            {
                Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample", Price = 1},
                }
            };

            IPromotionable promo = new Promotion3x1Fidelity();
            Assert.IsFalse(promo.IsApplicable(purchase));
        }

        [TestMethod]
        public void GivenApplicablePurchaseReturnsTrue()
        {
            Purchase purchase = new Purchase()
            {
                Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample", Price = 1},
                    new Product(){Brand = "brand sample", Price = 1},
                    new Product(){Brand = "brand sample", Price = 1},
                }
            };

            IPromotionable promo = new Promotion3x1Fidelity();
            Assert.IsTrue(promo.IsApplicable(purchase));
        }
    }
}
