using ApiModels.Out;
using BusinessLogic.Promotions;
using Domain;
using Domain.ProductParts;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Promotion20Off;
using Promotion3x1Fidelity;
using Promotion3x2;
using PromotionTotalLook;
using Domain.PaymentMethodCategories;

namespace WebApiModelsTest.Out
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreatePurchaseResponseTest
    {
        private Guid Id = Guid.NewGuid();
        private Guid BuyerId = Guid.NewGuid();
        private string promotion = "Promotion20Off";
        private CreatePurchaseResponse createPurchaseResponse;
        private int total = 20;
        private Purchase purchase;
        private List<Product> products;

        [TestInitialize]
        public void Init()
        {
            products = new List<Product>()
            {
                new Product()
                {
                    Name = "Test",
                    Brand = new Brand(){ Name = "brand"},
                    Category = new Category(){Name = "category"},
                    Colours =new List < Colour > () { new Colour() { Name = "Colour" } },
                    }
            };

            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                Cart = products,
                CurrentPromotion = promotion,
                PaymentMethod = new CreditCard() { CategoryName = "CreditCard", Flag = "Visa"}


            };


        }

        [TestMethod]
        public void GivenProductResponseReturnsGuid()
        {
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Id, createPurchaseResponse.Id);
        }
        [TestMethod]
        public void GivenProductResponseReturnsBuyerGuid()
        {
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.UserId, createPurchaseResponse.BuyerId);
        }
        [TestMethod]
        public void GivenProductResponseReturnsProductCorrectly()
        {
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Cart.First().Name, createPurchaseResponse.Cart.First().Name);
        }
        [TestMethod]
        public void GivenProductResponseReturnsCurrentPromotion()
        {
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.CurrentPromotion, createPurchaseResponse.SelectedPromotion);
        }

        [TestMethod]
        public void GivenProductResponseReturnsPurchaseDateTime()
        {
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Date, createPurchaseResponse.PurchaseTime);
        }


        [TestMethod]
        public void GivenProductResponseReturnsTotal()
        {
            purchase = new Purchase()
            {
                Total = total,
                PaymentMethod = new CreditCard() { CategoryName = "CreditCard", Flag = "Visa" }
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Total, createPurchaseResponse.Total);
        }



    }
}
