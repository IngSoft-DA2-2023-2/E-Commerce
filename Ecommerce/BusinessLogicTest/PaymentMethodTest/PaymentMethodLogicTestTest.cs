using BusinessLogic.PaymentMethod;
using LogicInterface;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.PaymentMethodTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class PaymentMethodLogicTestTest
    {
        private IPaymentMethod _bankDebitLogic;
        private readonly int totalCart = 100;
        private readonly string categoryName = "BankDebit";

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
