using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;

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
    }
}
