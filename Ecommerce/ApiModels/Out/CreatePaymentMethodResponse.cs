using Domain;
using Domain.PaymentMethodCategories;

namespace ApiModels.Out
{
    public class CreatePaymentMethodResponse
    {
        public string CategoryName { get; set; }
        public string? Bank { get; set; }
        public string? Flag { get; set; }

        public CreatePaymentMethodResponse(PaymentMethod paymentMethod)
        {
            CategoryName = paymentMethod.CategoryName;
            if (paymentMethod.GetType() == typeof(BankDebit)) Bank = ((BankDebit)paymentMethod).Bank;
            if (paymentMethod.GetType() == typeof(CreditCard)) Flag = ((CreditCard)paymentMethod).Flag;
        }
    }
}
