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
        private List<Product> _cartSample;

        [TestInitialize] 
        public void Initialize() {
            _cartSample = new List<Product>();
            _purchaseSample = new Purchase();
            _promo20Off = new Promotion20Off();
        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product());
           _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promo20Off.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given2ItemPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product());
            _cartSample.Add(new Product());

            _purchaseSample = new Purchase() {
                Cart = _cartSample,
            };

            Assert.IsTrue(_promo20Off.IsApplicable(_purchaseSample));
        }

    }
}
