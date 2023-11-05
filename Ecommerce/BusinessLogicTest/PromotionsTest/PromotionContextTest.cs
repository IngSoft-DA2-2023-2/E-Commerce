using BusinessLogic.Promotions;
using Domain;
using Domain.ProductParts;
using LogicInterface.Exceptions;
using System.Diagnostics.CodeAnalysis;
using Promotion20Off;
using Promotion3x1Fidelity;
using Promotion3x2;
using PromotionTotalLook;
using LogicInterface;

namespace BusinessLogicTest.PromotionsTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PromotionContextTest
    {
        Product product1;
        Product product2;
        Category category;
        PromotionContext promotionContext;
        private List<IPromotionable> promotions;
        [TestInitialize]
        public void Init()
        {
            promotionContext = new PromotionContext();
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

            promotions = new List<IPromotionable>();
            promotions.Add(new Promotion3x2Logic());
            promotions.Add(new Promotion20OffLogic());
            promotions.Add(new PromotionTotalLookLogic());
            promotions.Add(new Promotion3x1FidelityLogic());

            promotionContext.SetListPromotions(promotions);


    }

       
        [TestMethod]
        public void GivenPromotionableCartReturnsTrue()
        {
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,

            };
            Assert.IsTrue(promotionContext.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenNonPromotionableCartReturnsFalse()
        {
            List<Product> cart = new List<Product>()
            {
                product1
            };
            Assert.IsFalse(promotionContext.IsEligibleForPromotions(cart));
        }
        [TestMethod]
        public void GivenPromotionableCartReturnsBestPromotion()
        {
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,
            };
            Assert.AreEqual("20% Off", promotionContext.GetBestPromotion(cart));
        }

        [TestMethod]
        public void GivenPromotionableCartReturnsTotalWithBestPromotionApplied()
        {
            List<Product> cart = new List<Product>()
            {
                product1,
                product2,
            };
            Assert.AreEqual(12, promotionContext.CalculateTotalWithPromotion(cart));
        }
        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not Eligible for promotions")]
        public void GivenNonPromotionableCartThrowsException()
        {
            List<Product> cart = new List<Product>()
            { product1 };
            promotionContext.GetBestPromotion(cart);
        }
    }
}
