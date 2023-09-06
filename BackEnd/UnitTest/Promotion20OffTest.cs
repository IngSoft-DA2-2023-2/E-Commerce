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

        [TestMethod]
        public void Given3ItemPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product());
            _cartSample.Add(new Product());
            _cartSample.Add(new Product());

            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            Assert.IsTrue(_promo20Off.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException),"Not applicable promotion")]
        public void GivenNotApplicablePromotionThrowsBackEndException()
        {
            _cartSample.Add(new Product());
           
            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            _promo20Off.CalculateDiscount(_purchaseSample);
        }

        [TestMethod]
        public void Given2ItemPurchaseCalculatesDiscount()
        {
            _cartSample.Add(new Product() { Price = 100 }) ;
            _cartSample.Add(new Product() { Price = 50});

            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            Assert.AreEqual(100*0.2,_promo20Off.CalculateDiscount(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemPurchaseCalculatesDiscount()
        {
            _cartSample.Add(new Product() { Price = 100 });
            _cartSample.Add(new Product() { Price = 75 });
            _cartSample.Add(new Product() { Price = 50 });

            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            Assert.AreEqual(100 * .2, _promo20Off.CalculateDiscount(_purchaseSample));
        }
    }
}
