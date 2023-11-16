using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreatePurchaseRequestTest
    {
        private CreatePurchaseRequest purchaseRequest;
        private CreatePaymentMethodRequest createPaymentMethodRequest;
        private readonly Guid guid = Guid.NewGuid();
        private readonly List<CreateProductForPurchase> products = new List<CreateProductForPurchase>()
        {
            new CreateProductForPurchase()
            {
                Name = "name",
            }
        };

        [TestInitialize]
        public void Init()
        {
            purchaseRequest = new CreatePurchaseRequest();
            createPaymentMethodRequest = new CreatePaymentMethodRequest();
        }

        [TestMethod]
        public void GivenPurchaseRequestReturnsProductRequest()
        {
            purchaseRequest.Cart = products;
            Assert.AreEqual(purchaseRequest.Cart.First().Name, products.First().Name);
        }
    }
}