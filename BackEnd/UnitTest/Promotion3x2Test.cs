using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion3x2Test
    {
        private IPromotionable _promo3x2;
        private const string _categorySample1 = "category sample 1";
        private const string _categorySample2 = "category sample 2";
        private const string _categorySample3 = "category sample 3";

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

            Assert.IsFalse(_promo3x2.IsApplicable(purchase));
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

            Assert.IsTrue(_promo3x2.IsApplicable(purchase));
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
        public void GivenNonApplicablePurchaseThrowsBackEndException()
        {
            Product product1 = new Product() { Category = _categorySample1 };
            Purchase purchase = new Purchase() { Cart = new List<Product> { product1 } };

            _promo3x2.CalculateDiscount(purchase);
        }

        [TestMethod]
        public void Given3ItemsOfSameCategoryReturnsItsDiscount()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = 100 };
            Product product2 = new Product() { Category = _categorySample1, Price = 60 };
            Product product3 = new Product() { Category = _categorySample1, Price = 80 };

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

            Assert.AreEqual(60, _promo3x2.CalculateDiscount(purchase));
        }

        [TestMethod]
        public void Given3ItemsOfSameCategoryAndSamePriceReturnsItsDiscount()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = 100 };
            Product product2 = new Product() { Category = _categorySample1, Price = 100 };
            Product product3 = new Product() { Category = _categorySample1, Price = 100 };

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

            Assert.AreEqual(100, _promo3x2.CalculateDiscount(purchase));
        }

        [TestMethod]
        public void Given4ItemsOfSameCategoryAndSamePriceReturnsDiscountOfCheapest()
        {
            Product product1 = new Product() { Category = _categorySample1, Price = 1 };
            Product product2 = new Product() { Category = _categorySample1, Price = 2 };
            Product product3 = new Product() { Category = _categorySample1, Price = 3 };
            Product product4 = new Product() { Category = _categorySample1, Price = 4 };

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

            Assert.AreEqual(1, _promo3x2.CalculateDiscount(purchase));
        }

    }
}