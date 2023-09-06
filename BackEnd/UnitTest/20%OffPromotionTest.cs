using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PromotionTest
    {
        [TestMethod]
        public void GivenEmptyPurchaseReturnsPromotionIsNotApplicable()
        {
            Purchase p = new Purchase() {
                Cart = new List<Product>()
            };
            Promotion promotion = new Promotion();
            
            Assert.IsFalse(promotion.IsApplicable(p));
        }

        [TestMethod]
        public void Given2ItemPurchaseReturnsPromotionIsApplicable()
        {
            List<Product> products = new List<Product>() { 
            new Product(),
            new Product()
            };
            Purchase p = new Purchase() {
                Cart = products
            };
            Promotion promotion = new Promotion();

            Assert.IsTrue(promotion.IsApplicable(p));
        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            List<Product> products = new List<Product>() {
            new Product(),
            };
            Purchase p = new Purchase()
            {
                Cart = products
            };
            Promotion promotion = new Promotion();

            Assert.IsFalse(promotion.IsApplicable(p));
        }

    }
}
