using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreatePaymentMethodRequestTest
    {
        private CreatePaymentMethodRequest paymentMethodRequestCreditCard;
        private CreatePaymentMethodRequest paymentMethodRequestBankDebit;
        private readonly string categoryName = "CreditCard";
        private readonly string flag = "Visa";
        [TestInitialize]
        public void Init()
        {
            paymentMethodRequestCreditCard = new CreatePaymentMethodRequest()
            {
                CategoryName = "CreditCard",
                Flag = "Visa",

            };
            paymentMethodRequestBankDebit = new CreatePaymentMethodRequest()
            {
                CategoryName = "BankDebit",
                Bank = "Itau",
            };
        }
        [TestMethod]
        public void GivenPMCreditCardRequestReturnsPMRequest()
        {
            Assert.AreEqual(paymentMethodRequestCreditCard.CategoryName, categoryName);
            Assert.AreEqual(paymentMethodRequestCreditCard.Flag, flag);
        }

        [TestMethod]
        public void GivenPMBankDebitRequestReturnsPMRequest()
        {
            Assert.AreEqual(paymentMethodRequestBankDebit.CategoryName, "BankDebit");
            Assert.AreEqual(paymentMethodRequestBankDebit.Bank, "Itau");
        }
    }
}
