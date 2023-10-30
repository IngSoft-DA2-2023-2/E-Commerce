using BusinessLogic.PaymentMethod;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest.PaymentMethodTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaypalLogicTest
    {
        private IPaymentMethod _paypalLogic;
        private int totalCart = 100;
        private string categoryName = "Paypal";

        [TestInitialize]
        public void Init()
        {
            _paypalLogic = new PaypalLogic();
        }

        [TestMethod]
        public void GivenEmptyCartReturnsZero()
        {
            Assert.AreEqual(0, _paypalLogic.CalculateDiscount(0, categoryName));
        }
    }
}
