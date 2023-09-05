using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PurchaseTest
    {
        [TestMethod]
        public void GivenPurchaseReturnsUser()
        {
            User u = new User();

            Purchase p = new Purchase();
            p.User = u;

            Assert.AreEqual(u, p.User);
        }

        [TestMethod]
        public void GivenPurchaseReturnsProductsBought()
        {
            List<Product> productsBought = new List<Product>();
            Product product = new Product();
            productsBought.Add(product);
            

            Purchase p = new Purchase();
            p.Cart = productsBought;

           Assert.IsTrue(p.Cart.Count == 1);
            Assert.AreEqual(product, p.Cart[0]);
        }
    }
}
