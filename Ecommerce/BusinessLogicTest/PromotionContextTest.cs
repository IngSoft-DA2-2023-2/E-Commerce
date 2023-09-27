using BusinessLogic.Promotions;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class PromotionContextTest
    {
        [TestMethod]
        public void GivenPromotionableCartReturnsTrue()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                new Product()
                {
                    Name = "product1",
                    Description ="product1",
                    Category = "Category",
                },
                new Product()
                {
                    Name = "product2",
                    Description ="product2",
                    Category = "Category",
                },
                new Product()
                {
                    Name = "product3",
                    Description ="product3",
                    Category = "Category",
                }
            };
            Assert.IsTrue(promotion.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenNonPromotionableCartReturnsFalse()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                new Product()
                {
                    Name = "product1",
                    Description ="product1",
                    Category = "Category",
                },     
            };
            Assert.IsFalse(promotion.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenPromotionableCartReturnsBestPromotion()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                new Product()
                {
                    Name = "product1",
                    Price = 10,
                    Description ="product1",
                    Category = "Category",
                },
                new Product()
                {
                    Name = "product2",
                    Price = 5,
                    Description ="product2",
                    Category = "Category",
                },
                new Product()
                {
                    Name = "product3",
                    Price = 8,
                    Description ="product3",
                    Category = "Category",
                }
            };
            Assert.AreEqual("Fidelity",promotion.GetBestPromotion(cart));
        }
    }
}
