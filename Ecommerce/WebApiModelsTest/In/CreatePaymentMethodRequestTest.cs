using ApiModels.In;
using Domain.PaymentMethodCategories;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreatePaymentMethodRequestTest
    {
        private CreatePaymentMethodRequest paymentMethodRequestCreditCard;
        private CreatePaymentMethodRequest paymentMethodRequestBankDebit;
        private CreatePaymentMethodRequest paymentMethodRequestPaganza;
        private CreatePaymentMethodRequest paymentMethodRequestPaypal;
        private CreatePaymentMethodRequest invalidpaymentMethod;

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
            paymentMethodRequestPaganza = new CreatePaymentMethodRequest()
            {
                CategoryName = "Paganza"
            };
            paymentMethodRequestPaypal = new CreatePaymentMethodRequest()
            {
                CategoryName = "PayPal"
            };
            invalidpaymentMethod = new CreatePaymentMethodRequest();
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

        [TestMethod]
        public void ToEntityForBankDebit()
        {
            var res = paymentMethodRequestBankDebit.ToEntity();
            Assert.AreEqual(res.CategoryName, "BankDebit");
            Assert.AreEqual(((BankDebit)res).Bank, paymentMethodRequestBankDebit.Bank);
        }

        [TestMethod]
        public void ToEntityForPaganza()
        {
            var res = paymentMethodRequestPaganza.ToEntity();
            Assert.AreEqual(res.CategoryName, "Paganza");
        }

        [TestMethod]
        public void ToEntityForPaypal()
        {
            var res = paymentMethodRequestPaypal.ToEntity();
            Assert.AreEqual(res.CategoryName, "PayPal");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ToEntityThrowsExceptionIfIsInvalid()
        {
            invalidpaymentMethod.ToEntity();
        }


    }
}