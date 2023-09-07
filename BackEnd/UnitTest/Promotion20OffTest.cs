using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion20OffTest
    {
        private Promotion20Off _promo20Off;
        private Purchase _purchaseSample;
        private List<Product> _cartSample;

        private readonly Product _fiftyDollarProduct = new Product() { Price = 50 };
        private readonly Product _seventyFiveDollarProduct = new Product() { Price = 75 };
        private readonly Product _oneHundredDollarProduct = new Product() { Price = 100 };
        private const int _oneHundred = 100;
        private const float _twentyPercent = 0.2f;


        [TestInitialize]
        public void Initialize()
        {
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

            _purchaseSample = new Purchase()
            {
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
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
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
            _cartSample.Add(_oneHundredDollarProduct);
            _cartSample.Add(_fiftyDollarProduct);

            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            Assert.AreEqual(_oneHundred * _twentyPercent, _promo20Off.CalculateDiscount(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemPurchaseCalculatesDiscount()
        {
            _cartSample.Add(_oneHundredDollarProduct);
            _cartSample.Add(_seventyFiveDollarProduct);
            _cartSample.Add(_fiftyDollarProduct);

            _purchaseSample = new Purchase()
            {
                Cart = _cartSample,
            };

            Assert.AreEqual(_oneHundred * _twentyPercent, _promo20Off.CalculateDiscount(_purchaseSample));
        }
    }
}
