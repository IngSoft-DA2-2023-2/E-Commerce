using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PurchaseTest
    {
        private Purchase purchaseSample;
        private readonly User userSample = new User();
        private readonly Product productSample1 = new Product();
        private readonly Product productSample2 = new Product();


        [TestInitialize] 
        public void Initializer() {

            purchaseSample = new Purchase();
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
    }
}
