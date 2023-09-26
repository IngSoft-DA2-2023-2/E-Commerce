using BusinessLogic.Promotions;
using BusinessLogic.PurchaseLogic;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessLogicTest
{
    [TestClass]
    public class PurchaseLogicTest
    {
        private Purchase purchaseSample;
        private readonly User userSample = new User();
        private PurchaseLogic purchaseLogic;

        private readonly List<IPromotionable> promotionables = new List<IPromotionable>() {
            new Promotion20Off(),
            new Promotion3x2(),
            new PromotionTotalLook(),
            new Promotion3x1Fidelity(),
       };


        private readonly Product productSample1 = new Product()
        {
            Name = "name sample 1",
            Brand = "brand sample 1",
            Category = "category sample 1",
            Description = "description sample 1",
            Color = new List<string> { "color sample 1" },
            Price = 10,
        };

        private readonly Product productSample2 = new Product()
        {
            Name = "name sample 2",
            Brand = "brand sample 2",
            Category = "category sample 2",
            Description = "description sample 2",
            Color = new List<string> { "color sample 2" },
            Price = 20,
        };
        private readonly Product productSample3 = new Product()
        {
            Name = "name sample 3",
            Brand = "brand sample 3",
            Category = "category sample 3",
            Description = "description sample 3",
            Color = new List<string> { "color sample 3" },
            Price = 30,
        };

        [TestInitialize]
        public void Init()
        {

        }

        [TestMethod]
        public void GivenEmptyPurchaseReturnsFalse()
        {
            Assert.IsFalse(purchaseLogic.IsEligibleForPromotions(purchaseSample));
        }


        [TestMethod]
        public void Given1ItemPurchaseReturnsIsNotEligibleForPromotions()
        {

        }

        [TestMethod]
        public void Given3ItemPurchaseReturnsIsEligibleForPromotions()
        {
           
        }

        [TestMethod]
        public void Given3ItemPurchaseAssigns20OffPromotionAsBest()
        {
      
        }

        [TestMethod]
        public void GivenAssignedPromotionUnassignsIt()
        {
           
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not eligible for promotions")]
        public void GivenNotApplicableCartThrowsBackEndExceptionTryingToAssignBestPromotion()
        {

        }




    }
}
