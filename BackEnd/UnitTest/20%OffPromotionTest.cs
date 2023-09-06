using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;

namespace UnitTest
{
    [TestClass]
    public class PromotionTest
    {
        [TestMethod]
        public void GivenEmptyPurchaseReturnsPromotionIsNotApplicable()
        {
            Purchase p = new Purchase();
            Promotion promotion = new Promotion();
            
            Assert.IsFalse(promotion.IsApplicable(p));
        }
    }
}
