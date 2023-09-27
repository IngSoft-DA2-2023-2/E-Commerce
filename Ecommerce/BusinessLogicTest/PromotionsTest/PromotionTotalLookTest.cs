using BusinessLogic.Promotions;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogicTest.PromotionsTest
{
    [TestClass]
    public class PromotionTotalLookTest
    {
        private Purchase _purchaseSample;
        private List<Product> _cartSample;
        private IPromotionable _promotionTotalLook;

        private const string _red = "red";
        private const string _blue = "blue";
        private const string _green = "green";

        private const int _fifty = 50;
        private const int _seventy = 70;
        private const int _hundred = 100;
        private const int _twoHundred = 200;

        private const float _discount = 0.5f;

        [TestInitialize]
        public void Init()
        {
            _purchaseSample = new Purchase();
            _cartSample = new List<Product>();
            _promotionTotalLook = new PromotionTotalLook();
        }

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product());
            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample.Cart));
        }

        [TestMethod]
        public void Given2ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product());
            _cartSample.Add(new Product());

            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample.Cart));
        }

        [TestMethod]
        public void Given3ItemOfSameColorPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _blue, _green } });

            _purchaseSample.Cart = _cartSample;

            Assert.IsTrue(_promotionTotalLook.IsApplicable(_purchaseSample.Cart));
        }

        [TestMethod]
        public void Given3ItemOfDifferentColorsPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _green } });

            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample.Cart));
        }

        [TestMethod]
        public void Given4ItemOfSameColorsPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _red } });
            _cartSample.Add(new Product() { Color = new List<string> { _red, _green } });
            _cartSample.Add(new Product() { Color = new List<string> { _red } });

            _purchaseSample.Cart = _cartSample;

            Assert.IsTrue(_promotionTotalLook.IsApplicable(_purchaseSample.Cart));
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not applicable promotion")]
        public void GivenNonApplicablePromotionThrowsBackEndException()
        {
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue } });
            _cartSample.Add(new Product() { Color = new List<string> { _red } });

            _purchaseSample.Cart = _cartSample;

            _promotionTotalLook.CalculateDiscount(_purchaseSample.Cart);
        }

        [TestMethod]
        public void GivenApplicablePromotionReturnsDiscount()
        {

            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue }, Price = _hundred });
            _cartSample.Add(new Product() { Color = new List<string> { _red }, Price = _fifty });
            _cartSample.Add(new Product() { Color = new List<string> { _red }, Price = _seventy });

            _purchaseSample.Cart = _cartSample;

            Assert.AreEqual((int)(_hundred * _discount), _promotionTotalLook.CalculateDiscount(_purchaseSample.Cart));
        }

        public void GivenTwoPossibleApplicationReturnsHigherDiscount()
        {
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue }, Price = _hundred });
            _cartSample.Add(new Product() { Color = new List<string> { _blue }, Price = _twoHundred });
            _cartSample.Add(new Product() { Color = new List<string> { _red }, Price = _fifty });
            _cartSample.Add(new Product() { Color = new List<string> { _red, _blue }, Price = _seventy });

            _purchaseSample.Cart = _cartSample;

            Assert.AreEqual((int)(_twoHundred * _discount), _promotionTotalLook.CalculateDiscount(_purchaseSample.Cart));
        }
    }
}
