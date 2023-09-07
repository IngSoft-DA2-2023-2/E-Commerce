using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class Promotion3x2Test
    {
        [TestMethod]
        public void GivenItemsOfDifferentCategoriesReturnsIsNotApplicable()
        {
            Product product1 = new Product() { Category = "category sample 1" };
            Product product2 = new Product() { Category = "category sample 2" };
            Product product3 = new Product() { Category = "category sample 3" };

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

            Promotion3x2 promo = new Promotion3x2();
            Assert.IsFalse(promo.IsApplicable(purchase));
        }

        [TestMethod]
        public void GivenItemsOfSameCategoryReturnsIsApplicable()
        {
            Product product1 = new Product() { Category = "category sample 1" };
            Product product2 = new Product() { Category = "category sample 1" };
            Product product3 = new Product() { Category = "category sample 1" };

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

            Promotion3x2 promo = new Promotion3x2();
            Assert.IsTrue(promo.IsApplicable(purchase));
        }

        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
        public void GivenNonApplicablePurchaseThrowsBackEndException()
        {
            Product product1 = new Product() { Category = "category sample 1" };
            Purchase purchase = new Purchase() { Cart = new List<Product> { product1 } };

            Promotion3x2 promo = new Promotion3x2();
            promo.CalculateDiscount(purchase);
        }

        [TestMethod]
        public void GivenItemsOfSameCategoryReturnsItsDiscount()
        {
            Product product1 = new Product() { Category = "category sample 1" ,Price = 100};
            Product product2 = new Product() { Category = "category sample 1" ,Price = 60};
            Product product3 = new Product() { Category = "category sample 1" ,Price = 80};

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

            Promotion3x2 promo = new Promotion3x2();
            Assert.AreEqual(60,promo.CalculateDiscount(purchase));
        }
    }
}