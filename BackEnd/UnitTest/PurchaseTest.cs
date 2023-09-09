using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PurchaseTest
    {
        private Purchase purchaseSample;
        private readonly User userSample = new User();
        private readonly Product productSample1 = new Product()
        {
            Name = "name sample 1",
            Brand = "brand sample 1",
            Category = "category sample 1",
            Description = "description sample 1",
            Color = new List<String> { "color sample 1" },
            Price = 1,
        };

    private readonly Product productSample2 = new Product()
    {
        Name = "name sample 2",
        Brand = "brand sample 2",
        Category = "category sample 2",
        Description = "description sample 2",
        Color = new List<String> { "color sample 2" },
        Price = 2,
        };
        private readonly Product productSample3 = new Product()
        {
            Name = "name sample 3",
            Brand = "brand sample 3",
            Category = "category sample 3",
            Description = "description sample 3",
            Color = new List<String> { "color sample 3" },
            Price = 3,
        };

        private readonly List<IPromotionable> promotions = new List<IPromotionable> {
            new Promotion20Off(),
            new Promotion3x1Fidelity(),
            new Promotion3x2(),
            new PromotionTotalLook()
        };

        [TestInitialize]
        public void Init()
        {

            purchaseSample = new Purchase();
            purchaseSample.Promotions = promotions;
        }

        [TestMethod]
        public void GivenPurchaseReturnsUser()
        {
            purchaseSample.User = userSample;

            Assert.AreEqual(userSample, purchaseSample.User);
        }

        [TestMethod]
        public void GivenSingleItemPurchaseReturnsProductBought()
        {
            List<Product> productsBought = new List<Product>
            {
                productSample1
            };

            purchaseSample.Cart = productsBought;

            Assert.IsTrue(purchaseSample.Cart.Count == 1);
            Assert.AreEqual(productSample1, purchaseSample.Cart[0]);
        }

        [TestMethod]
        public void GivenTwoItemPurchaseReturnsProductsBought()
        {
            List<Product> productsBought = new List<Product>
            {
                productSample1,
                productSample2,
            };

            purchaseSample.Cart = productsBought;

            Assert.IsTrue(purchaseSample.Cart.Count == 2);
            Assert.AreEqual(productSample1, purchaseSample.Cart[0]);
            Assert.AreEqual(productSample2, purchaseSample.Cart[1]);

        }

        [TestMethod]
        public void GivenPurchaseReturnsItsDate()
        {
            DateTime now = DateTime.Now;
            purchaseSample.Date = now;

            Assert.AreEqual(now, purchaseSample.Date);
        }

        [TestMethod]
        public void GivenPurchaseReturnsPromotionUsed()
        {
            IPromotionable promotion = new Promotion20Off();
            purchaseSample.CurrentPromotion = promotion;

            Assert.AreEqual(promotion, purchaseSample.CurrentPromotion);
        }

        [TestMethod]
        public void GivenPurchaseReturnsNullAsDefaultPromotion()
        {
            Assert.AreEqual(null, purchaseSample.CurrentPromotion);
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Cart must not be null")]
        public void GivenNullCartThrowsBackEndException()
        {
            purchaseSample.Cart = null;
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Cart must not be empty")]
        public void GivenEmptyCartThrowsBackEndException()
        {
            purchaseSample.Cart = new List<Product>();
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Purchase Date must be before the current date")]
        public void GivenFutureDateThrowsBackEndException()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);
            purchaseSample.Date = tomorrow;
        }

        [TestMethod]
        public void GivenListOfPromotionsAssignsThemAsPromotionList()
        {
            IPromotionable promo1 = new Promotion20Off();
            IPromotionable promo2 = new Promotion3x2();
            IPromotionable promo3 = new PromotionTotalLook();
            IPromotionable promo4 = new Promotion20Off();

            List<IPromotionable> promotions = new List<IPromotionable>() {
            promo1,
            promo2,
            promo3,
            promo4
            };

            purchaseSample.Promotions = promotions;

            Assert.IsTrue(purchaseSample.Promotions.Count == 4);

            Assert.AreEqual(promo1, purchaseSample.Promotions[0]);
            Assert.AreEqual(promo2, purchaseSample.Promotions[1]);
            Assert.AreEqual(promo3, purchaseSample.Promotions[2]);
            Assert.AreEqual(promo4, purchaseSample.Promotions[3]);
        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsIsNotEligibleForPromotions()
        {
            List<Product> cart = new List<Product> { new Product() {
                Name = "name sample 3",
                Brand = "brand sample 3",
                Category = "category sample 3",
                Description = "description sample 3",
                Color = new List<String> { "color sample 3" },
            }
            };

            purchaseSample.Cart = cart;

            Assert.IsFalse(purchaseSample.IsEligibleForPromotions());
        }

        [TestMethod]
        public void Given3ItemPurchaseReturnsIsEligibleForPromotions()
        {
            purchaseSample.Cart = new List<Product> { productSample1 , productSample2 , productSample3 };

            Assert.IsTrue(purchaseSample.IsEligibleForPromotions());
        }
    }
}
