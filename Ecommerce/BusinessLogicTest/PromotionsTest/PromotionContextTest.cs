using BusinessLogic.Promotions;
using Domain;
using Domain.ProductParts;
using LogicInterface.Exceptions;
using System.Diagnostics.CodeAnalysis;
using Promotion20Off;
using Promotion3x1Fidelity;
using Promotion3x2;
using PromotionTotalLook;


namespace BusinessLogicTest.PromotionsTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PromotionContextTest
    {
        Product product1;
        Product product2;
        Category category;
        PromotionContext promotion;
       [TestInitialize]
        public void Init()
        {
            promotion = new PromotionContext();
            category = new Category() { Name = "category" };
            product1 = new Product()
            {
                Name = "product1",
                Description = "product1",
                Category = category,
                Brand = new Brand() { Name = "brand1" },
                Price = 10,
            };
            product2 = new Product()
            {
                Name = "product2",
                Description = "product2",
                Brand = new Brand() { Name = "brand2" },
                Category = category,
                Price = 4,
            };
            promotion.AddPromotion(new Promotion3x2Logic());
            promotion.AddPromotion(new Promotion20OffLogic());
            promotion.AddPromotion(new PromotionTotalLookLogic());
            promotion.AddPromotion(new Promotion3x1FidelityLogic());






        }

        [TestMethod]
        public void GivenPromotionableCartReturnsTrue()
        {
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
            List<Product> cart = new List<Product>()
            {
                product1
            };
            Assert.IsFalse(promotion.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenPromotionableCartReturnsBestPromotion()
        {
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,
            };
            Assert.AreEqual("20% Off", promotion.GetBestPromotion(cart));
        }

        [TestMethod]
        public void GivenPromotionableCartReturnsTotalWithBestPromotionApplied()
        {
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
            List<Product> cart = new List<Product>()
            { product1 };
            promotion.GetBestPromotion(cart);
        }
    }
}
