using BusinessLogic.Promotions;
using BusinessLogic.PurchaseLogic;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace UnitTest.BusinessLogicTest
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

        private readonly List<Promotion> promotions = new List<Promotion>()
        {
           new Promotion()
           {
               Name = "Promotion20off",
               Description = "Promotion20off"
           },
            new Promotion()
           {
               Name = "Promotion3x2",
               Description = "Promotion3x2"
           },
             new Promotion()
           {
               Name = "PromotionTotalLook",
               Description = "PromotionTotalLook"
           },
              new Promotion()
           {
               Name = "Promotion3x1Fidelity",
               Description = "Promotion3x1Fidelity"
           }

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
            purchaseSample = new Purchase
            {
                Promotions = promotions

            };
            Mock<IPromotionLogic> mock = new Mock<IPromotionLogic>();
            mock.Setup(x => x.GetPromotionable(It.Is<Promotion>(p => p.Equals(promotions[0])))).Returns(promotionables[0]); //si me das la promocion 0 yo te doy esa estrategia
            mock.Setup(x => x.GetPromotionable(It.Is<Promotion>(p => p.Equals(promotions[1])))).Returns(promotionables[1]);
            mock.Setup(x => x.GetPromotionable(It.Is<Promotion>(p => p.Equals(promotions[2])))).Returns(promotionables[2]);
            mock.Setup(x => x.GetPromotionable(It.Is<Promotion>(p => p.Equals(promotions[3])))).Returns(promotionables[3]);
            purchaseLogic = new PurchaseLogic(mock.Object);
        }

        [TestMethod]
        public void GivenEmptyPurchaseReturnsFalse()
        {
            Assert.IsFalse(purchaseLogic.IsEligibleForPromotions(purchaseSample));
        }


        [TestMethod]
        public void Given1ItemPurchaseReturnsIsNotEligibleForPromotions()
        {

            List<Product> cart = new List<Product> { productSample3 };
            purchaseSample.Cart = cart;

            Assert.IsFalse(purchaseLogic.IsEligibleForPromotions(purchaseSample));
        }

        [TestMethod]
        public void Given3ItemPurchaseReturnsIsEligibleForPromotions()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };

            Assert.IsTrue(purchaseLogic.IsEligibleForPromotions(purchaseSample));
        }

        [TestMethod]
        public void Given3ItemPurchaseAssigns20OffPromotionAsBest()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };
            purchaseLogic.AssignsBestPromotion(purchaseSample);

            Assert.AreEqual(purchaseSample.CurrentPromotion, promotions[0]);
        }

        [TestMethod]
        public void GivenAssignedPromotionUnassignsIt()
        {
            purchaseSample.Cart = new List<Product> { productSample1, productSample2, productSample3 };
            purchaseLogic.AssignsBestPromotion(purchaseSample);
            Assert.IsNotNull(purchaseSample.CurrentPromotion);

            purchaseSample.DropPromotion();

            Assert.IsNull(purchaseSample.CurrentPromotion);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException), "Not eligible for promotions")]
        public void GivenNotApplicableCartThrowsBackEndExceptionTryingToAssignBestPromotion()
        {
            purchaseSample.Cart = new List<Product>() { productSample1 };
            Assert.IsFalse(purchaseLogic.IsEligibleForPromotions(purchaseSample));

            purchaseLogic.AssignsBestPromotion(purchaseSample);
        }



    }
}
