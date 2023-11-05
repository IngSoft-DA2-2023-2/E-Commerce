using BusinessLogic.PaymentMethod;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.PaymentMethodTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaymentMethodContextText
    {
        private PaymentMethodContext _paymentMethodContext;

        [TestInitialize]
        public void Init()
        {
            _paymentMethodContext = new PaymentMethodContext();
        }

        [TestMethod]
        public void GivenEmptyCartReturnsZero()
        {
            Assert.AreEqual(0, _paymentMethodContext.CalculateDiscount(0, "Paganza"));
        }

        [TestMethod]
        public void GivenCartWithOnePaganzaReturnsDiscount()
        {
            Assert.AreEqual(90, _paymentMethodContext.CalculateDiscount(100, "Paganza"));
        }


    }
}
