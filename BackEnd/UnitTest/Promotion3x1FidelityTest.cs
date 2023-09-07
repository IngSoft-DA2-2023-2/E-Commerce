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

        [TestInitialize]
        public void Init()
        {
            _purchaseSample = new Purchase();

        }

        [TestMethod]
        public void GivenOneItemReturnsDiscountIsNotApplicable()
        {
            List<Product> products = new List<Product> { new Product() { Brand = "brand sample", Price = 1 } };

            _purchaseSample.Cart = products;
                    

            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenThreeItemsOfSameBrandReturnsDiscountIsApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample", Price = 1},
                    new Product(){Brand = "brand sample", Price = 1},
                    new Product(){Brand = "brand sample", Price = 1},
                };
            

            Assert.IsTrue(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenThreeItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample 1", Price = 1},
                    new Product(){Brand = "brand sample 2", Price = 1},
                    new Product(){Brand = "brand sample 3", Price = 1},
                };
           

            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }

        [TestMethod]
        public void GivenFourItemsOfDifferentBrandsReturnsDiscountIsNotApplicable()
        {

            _purchaseSample.Cart = new List<Product>
                {
                    new Product(){Brand = "brand sample 1", Price = 1},
                    new Product(){Brand = "brand sample 2", Price = 1},
                    new Product(){Brand = "brand sample 3", Price = 1},
                    new Product(){Brand = "brand sample 4", Price = 1}
                };
            
            Assert.IsFalse(_promo.IsApplicable(_purchaseSample));
        }
    }
}
