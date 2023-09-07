using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PromotionTotalLookTest
    {
        private Purchase _purchaseSample;
        private List<Product> _cartSample;
        private PromotionTotalLook _promotionTotalLook;

        [TestInitialize]
        public void Init() {
        _purchaseSample = new Purchase();
        _cartSample = new List<Product>();
        _promotionTotalLook = new PromotionTotalLook();

    }

        

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        { 
            _cartSample.Add(new Product());
            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given2ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product());
            _cartSample.Add(new Product());

            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemOfSameColorPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "blue", "green" } });
           
            _purchaseSample.Cart = _cartSample;

            Assert.IsTrue(_promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemOfDifferentColorsPurchaseReturnsPromotionIsNotApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "green" } });

            _purchaseSample.Cart = _cartSample;

            Assert.IsFalse(_promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given4ItemOfSameColorsPurchaseReturnsPromotionIsApplicable()
        {
            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "red" } });
            _cartSample.Add(new Product() { Color = new List<string> { "red", "green" } });
            _cartSample.Add(new Product() { Color = new List<string> { "red" } });

            _purchaseSample.Cart = _cartSample;

            Assert.IsTrue(_promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
        public void GivenNonApplicablePromotionThrowsBackEndException()
        {

            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" } });
            _cartSample.Add(new Product() { Color = new List<string> { "red" } });
            

            _purchaseSample.Cart = _cartSample;

            _promotionTotalLook.CalculateDiscount(_purchaseSample);
        }

        [TestMethod]
        public void GivenApplicablePromotionReturnsDiscount()
        {

            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" }, Price = 100 });
            _cartSample.Add(new Product() { Color = new List<string> { "red" }, Price = 50 });
            _cartSample.Add(new Product() { Color = new List<string> { "red" }, Price = 80 });
            
            _purchaseSample.Cart = _cartSample;

            Assert.AreEqual(((int)(100 * .5f)), _promotionTotalLook.CalculateDiscount(_purchaseSample));
        }
        public void GivenTwoPossibleApplicationReturnsHigherDiscount()
        {

            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" }, Price = 100 });
            _cartSample.Add(new Product() { Color = new List<string> { "blue" }, Price = 200 });
            _cartSample.Add(new Product() { Color = new List<string> { "red" }, Price = 50 });
            _cartSample.Add(new Product() { Color = new List<string> { "red", "blue" }, Price = 80 });
            
            _purchaseSample.Cart = _cartSample;

            Assert.AreEqual(((int)(200 * .5f)), _promotionTotalLook.CalculateDiscount(_purchaseSample));
        }
    }
}
