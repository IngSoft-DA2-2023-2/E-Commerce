using WebApi.ExceptionBackEnd;
using WebApi.BusinessLogic.Promotions;
using WebApi.Domain;
using WebApi.LogicInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace UnitTest.DomainTest
{
    [TestClass]
    public class PurchaseTest
    {
        private Purchase purchaseSample;
        private readonly User userSample = new User();

        private readonly List<IPromotionable> promotions = new List<IPromotionable>() {
            new Promotion20Off(),
            new Promotion3x2(),
            new PromotionTotalLook(),
            new Promotion3x1Fidelity(),
       };

        private readonly Product productSample1 = new Product()
        {
            Name = "name sample 1",
            Brand = "brand sample 1",
            Category = "category sample 1",
            Description = "description sample 1",
            Color = new List<string> { "color sample 1" },
            Price = 10,
        };

        private readonly Product productSample2 = new Product()
        {
            Name = "name sample 2",
            Brand = "brand sample 2",
            Category = "category sample 2",
            Description = "description sample 2",
            Color = new List<string> { "color sample 2" },
            Price = 20,
        };
        private readonly Product productSample3 = new Product()
        {
            Name = "name sample 3",
            Brand = "brand sample 3",
            Category = "category sample 3",
            Description = "description sample 3",
            Color = new List<string> { "color sample 3" },
            Price = 30,
        };

        [TestInitialize]
        public void Init()
        {
            purchaseSample = new Purchase
            {
                Promotions = promotions
            };
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
            purchaseSample.Promotions = promotions;

            Assert.IsTrue(purchaseSample.Promotions.Count == 4);

            Assert.AreEqual(typeof(Promotion20Off), purchaseSample.Promotions[0].GetType());
            Assert.AreEqual(typeof(Promotion3x2), purchaseSample.Promotions[1].GetType());
            Assert.AreEqual(typeof(PromotionTotalLook), purchaseSample.Promotions[2].GetType());
            Assert.AreEqual(typeof(Promotion3x1Fidelity), purchaseSample.Promotions[3].GetType());
        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsIsNotEligibleForPromotions()
        {
            List<Product> cart = new List<Product> { productSample3 };
            purchaseSample.Cart = cart;

            Assert.IsFalse(purchaseSample.IsEligibleForPromotions());
        }

        [TestMethod]
        public void Given3ItemPurchaseReturnsIsEligibleForPromotions()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };

            Assert.IsTrue(purchaseSample.IsEligibleForPromotions());
        }

        [TestMethod]
        public void Given3ItemPurchaseAssigns20OffPromotionAsBest()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };
            purchaseSample.AssignsBestPromotion();

            Assert.IsInstanceOfType(purchaseSample.CurrentPromotion, typeof(Promotion20Off));
        }

        [TestMethod]
        public void GivenAssignedPromotionUnassignsIt()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };
            purchaseSample.AssignsBestPromotion();
            Assert.IsNotNull(purchaseSample.CurrentPromotion);

            purchaseSample.DropPromotion();

            Assert.IsNull(purchaseSample.CurrentPromotion);
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not eligible for promotions")]
        public void GivenNotApplicableCartThrowsBackEndExceptionTryingToAssignBestPromotion()
        {
            purchaseSample.Cart = new List<Product>() { productSample1 };
            Assert.IsFalse(purchaseSample.IsEligibleForPromotions());

            purchaseSample.AssignsBestPromotion();
        }
    }
}
