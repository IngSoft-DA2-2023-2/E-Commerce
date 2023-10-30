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
    public class CreditCardLogicTest
    {
        private IPaymentMethod _creditCardLogic;
        private int totalCart = 100;
        private string categoryName = "CreditCard";

        [TestInitialize]
        public void Init()
        {
            _creditCardLogic = new CreditCardLogic();
        }

        [TestMethod]
        public void GivenEmptyCartReturnsZero()
        {
            Assert.AreEqual(0, _creditCardLogic.CalculateDiscount(0, categoryName));
        }
    }
}
