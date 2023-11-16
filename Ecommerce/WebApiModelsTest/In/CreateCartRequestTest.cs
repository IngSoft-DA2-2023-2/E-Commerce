using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateCartRequestTest
    {
        [TestMethod]
        public void GivenCreateCartRequestReturnsItsCart()
        {
            List<CreateProductRequest> cart = new List<CreateProductRequest>();
            CreateCartRequest createCartRequest = new CreateCartRequest();
            createCartRequest.Cart = cart;
            Assert.AreEqual(cart, createCartRequest.Cart);
        }
    }
}