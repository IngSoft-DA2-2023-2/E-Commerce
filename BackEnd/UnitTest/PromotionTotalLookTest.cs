using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [ExpectedException(typeof(BackEndException),"Not applicable promotion")]
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
    }
}
