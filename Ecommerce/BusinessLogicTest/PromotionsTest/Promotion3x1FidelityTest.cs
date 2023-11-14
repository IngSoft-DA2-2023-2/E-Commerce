using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using Promotion3x1Fidelity;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.PromotionsTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class Promotion3x1FidelityTest
    {
        private readonly IPromotionable _promo = new Promotion3x1FidelityLogic();
        private Purchase _purchaseSample;

        private const int _one = 1;
        private const int _two = 2;
        private const int _three = 3;
        private const int _four = 4;
        private const int _five = 5;
        private const int _six = 6;

        private readonly Brand _brandSample1 = new Brand() { Name = "brand sample" };
        private readonly Brand _brandSample2 = new Brand() { Name = "brand sample 2" };
        private readonly Brand _brandSample3 = new Brand() { Name = "brand sample 3" };
        private readonly Brand _brandSample4 = new Brand() { Name = "brand sample 4" };

        private const int _discount2 = 2;
        private const int _discount3 = 3;

        [TestInitialize]
        public void Init()
        {
            _purchaseSample = new Purchase();

        }

        [TestMethod]
        public void GivenOneItemReturnsDiscountIsNotApplicable()
        {
            List<Product> cart = new List<Product>() { new Product()
            { Brand = _brandSample1,
              Price = _one } };

            Assert.IsFalse(_promo.IsApplicable(cart));
        }

        [TestMethod]
        public void GivenThreeItemsOfSameBrandReturnsDiscountIsApplicable()
        {

            List<Product> cartSample = new List<Product>()
            {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                };


            Assert.IsTrue(_promo.IsApplicable(cartSample));
        }

        [TestMethod]
        public void GivenThreeItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            List<Product> cartSample = new List<Product>()
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample2, Price = _one},
                    new Product(){Brand = _brandSample3, Price = _one},
                };


            Assert.IsFalse(_promo.IsApplicable(cartSample));
        }

        [TestMethod]
        public void GivenFourItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            List<Product> cartSample = new()
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample2, Price = _one},
                    new Product(){Brand = _brandSample3, Price = _one},
                    new Product(){Brand = _brandSample4, Price = _one}
                };

            Assert.IsFalse(_promo.IsApplicable(cartSample));
        }


        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not applicable promotion")]
        public void GivenNotApplicablePromotionThrowsBackEndException()
        {
            Product productSample = new Product()
            {
                Name = "product2",
                Description = "product2",
                Brand = new Brand() { Name = "brand2" },
                Category = new Category { Name = "category2" },
                Price = _four,
            };
            List<Product> cartSample = new List<Product>() { productSample };

            _promo.CalculateDiscount(cartSample);
        }

        [TestMethod]
        public void GivenThreeItemsOfSameBrandCalculatesItsDiscount()
        {

            List<Product> cartSample = new()
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                };
            Assert.AreEqual(_discount2, _promo.CalculateDiscount(cartSample));
        }

        [TestMethod]
        public void GivenSixItemsOfSameBrandCalculatesItsDiscount()
        {

            List<Product> cartSample = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _two},
                    new Product(){Brand = _brandSample1, Price = _three},
                    new Product(){Brand = _brandSample1, Price = _four},
                    new Product(){Brand = _brandSample1, Price = _five},
                    new Product(){Brand = _brandSample1, Price = _six},
                };
            Assert.AreEqual(_discount3, _promo.CalculateDiscount(cartSample));
        }
    }
}