using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion3x1FidelityTest
    {
        [TestMethod]
        public void GivenOneItemReturnsDiscountIsNotApplicable()
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
        public void GivenThreeItemsOfSameBrandReturnsDiscountIsApplicable()
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

        [TestMethod]
        public void GivenThreeItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {
            Purchase purchase = new Purchase()
            {
                Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample 1", Price = 1},
                    new Product(){Brand = "brand sample 2", Price = 1},
                    new Product(){Brand = "brand sample 3", Price = 1},
                }
            };

            IPromotionable promo = new Promotion3x1Fidelity();
            Assert.IsFalse(promo.IsApplicable(purchase));
        }
    }
}
