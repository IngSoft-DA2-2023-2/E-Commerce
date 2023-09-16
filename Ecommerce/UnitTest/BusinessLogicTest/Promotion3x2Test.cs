using BusinessLogic.Exceptions;
using Domain;
using BusinessLogic.Promotions;
using LogicInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest.BusinessLogicTest
{
    [TestClass]
    public class Promotion3x2Test
    {
        private IPromotionable _promo3x2;
        private const string _categorySample1 = "category sample 1";
        private const string _categorySample2 = "category sample 2";
        private const string _categorySample3 = "category sample 3";

        private const int _one = 1;
        private const int _two = 2;
        private const int _three = 3;
        private const int _four = 4;

        [TestInitialize]
        public void Init()
        {
            _promo3x2 = new Promotion3x2();
        }

        [TestMethod]
        public void GivenItemsOfDifferentCategoriesReturnsIsNotApplicable()
        {
            Product product1 = new Product() { Category = _categorySample1 };
            Product product2 = new Product() { Category = _categorySample2 };
            Product product3 = new Product() { Category = _categorySample3 };

            List<Product> products = new List<Product>
            {
                product1,
                product2,
                product3
            };

            Purchase purchase = new Purchase()
            {
                Cart = products

            };

            Assert.IsFalse(_promo3x2.IsApplicable(purchase.Cart));
        }

        [TestMethod]
        public void GivenItemsOfSameCategoryReturnsIsApplicable()
        {
            Product product1 = new Product() { Category = _categorySample1 };
            Product product2 = new Product() { Category = _categorySample1 };
            Product product3 = new Product() { Category = _categorySample1 };

            List<Product> products = new List<Product>
            {
                product1,
                product2,
                product3
            };

            Purchase purchase = new Purchase()
            {
                Cart = products
            };

            Assert.IsTrue(_promo3x2.IsApplicable(purchase.Cart));
        }

        [TestMethod]
        [ExpectedException(typeof(BusinessLogicException), "Not applicable promotion")]
        public void GivenNonApplicablePurchaseThrowsBackEndException()
        {
            Product product1 = new Product() { Category = _categorySample1 };
            Purchase purchase = new Purchase() { Cart = new List<Product> { product1 } };

            _promo3x2.CalculateDiscount(purchase.Cart);
        }

        [TestMethod]
        public void Given3ItemsOfSameCategoryReturnsItsDiscount()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = _one };
            Product product2 = new Product() { Category = _categorySample1, Price = _two };
            Product product3 = new Product() { Category = _categorySample1, Price = _three };

            List<Product> products = new List<Product>
            {
                product1,
                product2,
                product3
            };

            Purchase purchase = new Purchase()
            {
                Cart = products
            };

            Assert.AreEqual(_one, _promo3x2.CalculateDiscount(purchase.Cart));
        }

        [TestMethod]
        public void Given3ItemsOfSameCategoryAndSamePriceReturnsItsDiscount()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = _three };
            Product product2 = new Product() { Category = _categorySample1, Price = _three };
            Product product3 = new Product() { Category = _categorySample1, Price = _three };

            List<Product> products = new List<Product>
            {
                product1,
                product2,
                product3
            };

            Purchase purchase = new Purchase()
            {
                Cart = products
            };

            Assert.AreEqual(_three, _promo3x2.CalculateDiscount(purchase.Cart));
        }

        [TestMethod]
        public void Given4ItemsOfSameCategoryAndSamePriceReturnsDiscountOfCheapest()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = _one };
            Product product2 = new Product() { Category = _categorySample1, Price = _two };
            Product product3 = new Product() { Category = _categorySample1, Price = _three };
            Product product4 = new Product() { Category = _categorySample1, Price = _four };

            List<Product> products = new List<Product>
            {
                product1,
                product2,
                product3,
                product4
            };

            Purchase purchase = new Purchase()
            {
                Cart = products
            };

            Assert.AreEqual(_one, _promo3x2.CalculateDiscount(purchase.Cart));
        }

    }
}