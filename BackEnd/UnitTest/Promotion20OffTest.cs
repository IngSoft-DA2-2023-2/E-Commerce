using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BackEnd;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion20OffTest
    {
        private Promotion20Off _promo20Off;
        private Purchase _purchaseSample;
        private List<Product> products;

        [TestInitialize] 
        public void Initialize() {
            products = new List<Product>();

        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            products.Add(new Product());
           
           _purchaseSample.Cart = products;

            Promotion20Off promotion = new Promotion20Off();

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
            Promotion20Off promotion = new Promotion20Off();

            Assert.IsTrue(promotion.IsApplicable(p));
        }

    }
}
