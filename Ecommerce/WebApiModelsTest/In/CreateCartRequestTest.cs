using ApiModels.In;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
