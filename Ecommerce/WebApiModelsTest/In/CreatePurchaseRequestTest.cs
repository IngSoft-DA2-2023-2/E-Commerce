using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreatePurchaseRequestTest
    {
        private CreatePurchaseRequest purchaseRequest;
        Guid guid = Guid.NewGuid();
        private List<CreateProductRequest> products = new List<CreateProductRequest>()
        {
            new CreateProductRequest()
            {
                Name = "name",
            }
        };

        [TestInitialize]
        public void Init()
        {
            purchaseRequest = new CreatePurchaseRequest();
        }
        [TestMethod]
        public void GivenPurchaseRequestReturnsProductRequest()
        {
            purchaseRequest.Cart = products;
            Assert.AreEqual(purchaseRequest.Cart.First().Name, products.First().Name);
        }

    }
}
