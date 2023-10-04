using ApiModels.Out;
using Domain;
using Domain.ProductParts;

namespace WebApiModelsTest.Out
{

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
        }

        [TestMethod]
        public void GivenProductResponseReturnsGuid()
        {
            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                Cart = products,
                CurrentPromotion = promotion,
                
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Id, createPurchaseResponse.Id);
        }
        [TestMethod]
        public void GivenProductResponseReturnsBuyerGuid()
        {
            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                
                Cart = products,
                CurrentPromotion = promotion,
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.UserId, createPurchaseResponse.BuyerId);
        }
        [TestMethod]
        public void GivenProductResponseReturnsProductCorrectly()
        {
            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                Cart = products,
                CurrentPromotion = promotion,
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Cart.First().Name, createPurchaseResponse.Cart.First().Name);
        }
        [TestMethod]
        public void GivenProductResponseReturnsCurrentPromotion()
        {
            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                Cart = products,
                CurrentPromotion = promotion,
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.CurrentPromotion, createPurchaseResponse.SelectedPromotion);
        }

        [TestMethod]
        public void GivenProductResponseReturnsPurchaseDateTime()
        {
            purchase = new Purchase()
            {
                Id = Id,
                UserId = BuyerId,
                Cart = products,
                CurrentPromotion = promotion,
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Date, createPurchaseResponse.PurchaseTime);
        }


        [TestMethod]
        public void GivenProductResponseReturnsTotal()
        {
            purchase = new Purchase()
            {
                Total = total
            };
            createPurchaseResponse = new CreatePurchaseResponse(purchase);
            Assert.AreEqual(purchase.Total, createPurchaseResponse.Total);
        }



    }
}
