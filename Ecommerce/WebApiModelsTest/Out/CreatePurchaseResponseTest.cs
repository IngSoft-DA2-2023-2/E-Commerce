using ApiModels.Out;
using Domain;
using Domain.ProductParts;

namespace WebApiModelsTest.Out
{

    [TestClass]
    public class CreatePurchaseResponseTest
    {
        public Guid Id = Guid.NewGuid();
        public Guid BuyerId = Guid.NewGuid();
        public string promotion = "Promotion20Off";
        public CreatePurchaseResponse createPurchaseResponse;
        Purchase purchase;
        List<Product> products;

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
                    Colors =new List < Colour > () { new Colour() { Name = "Colour" } }

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




    }
}
