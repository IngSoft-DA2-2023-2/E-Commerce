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
    public class PaymentMethodLogicTestTest
    {
        private IPaymentMethod _bankDebitLogic;
        private int totalCart = 100;
        private string categoryName = "BankDebit";

        [TestInitialize]
        public void Init()
        {
            _bankDebitLogic = new BankDebitLogic();
        }

        [TestMethod]
        public void GivenEmptyCartReturnsZero()
        {
            Assert.AreEqual(0, _bankDebitLogic.CalculateDiscount(0, categoryName));
        }
    }
}
