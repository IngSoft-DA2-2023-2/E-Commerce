using Domain;
using Domain.Exceptions;
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

        private readonly List<Promotion> promotions = new List<Promotion>()
        {
           new Promotion()
           {
               Name = "Promotion20off",
               Description = "Promotion20off"
           },
            new Promotion()
           {
               Name = "Promotion3x2",
               Description = "Promotion3x2"
           },
             new Promotion()
           {
               Name = "PromotionTotalLook",
               Description = "PromotionTotalLook"
           },
              new Promotion()
           {
               Name = "Promotion3x1Fidelity",
               Description = "Promotion3x1Fidelity"
           }

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
        [ExpectedException(typeof(DomainException), "Cart must not be null")]
        public void GivenNullCartThrowsBackEndException()
        {
            purchaseSample.Cart = null;
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Cart must not be empty")]
        public void GivenEmptyCartThrowsBackEndException()
        {
            purchaseSample.Cart = new List<Product>();
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Purchase Date must be before the current date")]
        public void GivenFutureDateThrowsBackEndException()
        {
            DateTime tomorrow = DateTime.Now.AddDays(1);
            purchaseSample.Date = tomorrow;
        }


    }
}
