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
