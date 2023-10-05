using BusinessLogic.Promotions;
using Domain;
using Domain.ProductParts;
using LogicInterface.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.PromotionsTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PromotionContextTest
    {
        Product product1;
        Product product2;
        Category category;
        [TestInitialize]
        public void Init()
        {
            category = new Category() { Name = "category" };
            product1 = new Product()
            {
                Name = "product1",
                Description = "product1",
                Category = category,
                Price = 10,
            };
            product2 = new Product()
            {
                Name = "product2",
                Description = "product2",
                Category = category,
                Price = 4,
            };
        }
        [TestMethod]
        public void GivenPromotionableCartReturnsTrue()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,

            };
            Assert.IsTrue(promotion.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenNonPromotionableCartReturnsFalse()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                product1
            };
            Assert.IsFalse(promotion.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenPromotionableCartReturnsBestPromotion()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,
            };
            Assert.AreEqual("PercentageOff", promotion.GetBestPromotion(cart));
        }

        [TestMethod]
        public void GivenPromotionableCartReturnsTotalWithBestPromotionApplied()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,
            };
            Assert.AreEqual(12, promotion.CalculateTotalWithPromotion(cart));
        }
        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not Eligible for promotions")]
        public void GivenNonPromotionableCartThrowsException()
        {
            PromotionContext promotion = new PromotionContext();
            List<Product> cart = new List<Product>()
            { product1 };
            promotion.GetBestPromotion(cart);
        }
    }
}
