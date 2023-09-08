using BackEnd;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace UnitTest
{
    [TestClass]
    public class Promotion3x1FidelityTest
    {
        private readonly IPromotionable _promo = new Promotion3x1Fidelity();
        private Purchase _purchaseSample;

        private const int _one = 1;
        private const int _two = 2;
        private const int _three = 3;
        private const int _four = 4;
        private const int _five = 5;
        private const int _six = 6;

        private const string _brandSample1 = "brand sample";
        private const string _brandSample2 = "brand sample 2";
        private const string _brandSample3 = "brand sample 3";
        private const string _brandSample4 = "brand sample 4";

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
            List<Product> products = new List<Product> { new Product() { Brand = _brandSample1, Price = _one } };

            _purchaseSample.Cart = products;


            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenThreeItemsOfSameBrandReturnsDiscountIsApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                };


            Assert.IsTrue(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenThreeItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample2, Price = _one},
                    new Product(){Brand = _brandSample3, Price = _one},
                };


            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenFourItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample2, Price = _one},
                    new Product(){Brand = _brandSample3, Price = _one},
                    new Product(){Brand = _brandSample4, Price = _one}
                };

            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }


        [TestMethod]
        [ExpectedException(typeof(BackEndException), "Not applicable promotion")]
        public void GivenNotApplicablePromotionThrowsBackEndException()
        {
            _purchaseSample.Cart = new List<Product> { (new Product()) };

            _promo.CalculateDiscount(_purchaseSample);
        }

        [TestMethod]
        public void GivenThreeItemsOfSameBrandCalculatesItsDiscount()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _one},
                };
            Assert.AreEqual(_discount2, _promo.CalculateDiscount(_purchaseSample));
        }

        [TestMethod]
        public void GivenSixItemsOfSameBrandCalculatesItsDiscount()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = _brandSample1, Price = _one},
                    new Product(){Brand = _brandSample1, Price = _two},
                    new Product(){Brand = _brandSample1, Price = _three},
                    new Product(){Brand = _brandSample1, Price = _four},
                    new Product(){Brand = _brandSample1, Price = _five},
                    new Product(){Brand = _brandSample1, Price = _six},
                };
            Assert.AreEqual(_discount3, _promo.CalculateDiscount(_purchaseSample));
        }
    }
}
