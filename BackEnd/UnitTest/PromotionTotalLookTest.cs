using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class PromotionTotalLookTest
    {

        [TestMethod]
        public void Given1ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>();
            _cartSample.Add(new Product());
            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.IsFalse(promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given2ItemPurchaseReturnsPromotionIsNotApplicable()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product(),
                new Product()
            };
            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.IsFalse(promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemOfSameColorPurchaseReturnsPromotionIsApplicable()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product() { Color = new List<string> { "red", "blue" } },
                new Product() { Color = new List<string> { "blue" } },
                new Product() { Color = new List<string> { "blue", "green" } }
            };

            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.IsTrue(promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given3ItemOfDifferentColorsPurchaseReturnsPromotionIsApplicable()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product() { Color = new List<string> { "red", "blue" } },
                new Product() { Color = new List<string> { "blue" } },
                new Product() { Color = new List<string> { "red", "green" } }
            };

            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.IsFalse(promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void Given4ItemOfSameColorsPurchaseReturnsPromotionIsApplicable()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product() { Color = new List<string> { "red", "blue" } },
                new Product() { Color = new List<string> { "red" } },
                new Product() { Color = new List<string> { "red", "green" } },
                new Product() { Color = new List<string> { "red" } }
            };

            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.IsTrue(promotionTotalLook.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
        public void GivenNonApplicablePromotionThrowsBackEndException()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product() { Color = new List<string> { "red", "blue" } },
                new Product() { Color = new List<string> { "red" } },
            };

            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            promotionTotalLook.CalculateDiscount(_purchaseSample);

        }

        [TestMethod]
        public void GivenApplicablePromotionReturnsDiscount()
        {
            Purchase _purchaseSample = new Purchase();
            List<Product> _cartSample = new List<Product>
            {
                new Product() { Color = new List<string> { "red", "blue" },Price = 100 },
                new Product() { Color = new List<string> { "red" } , Price = 50 },
                new Product() { Color = new List<string> { "red" }, Price = 80 },
            };

            _purchaseSample.Cart = _cartSample;

            PromotionTotalLook promotionTotalLook = new PromotionTotalLook();
            Assert.AreEqual(((int)(100 * .5f)), promotionTotalLook.CalculateDiscount(_purchaseSample));

        }
    }
}
