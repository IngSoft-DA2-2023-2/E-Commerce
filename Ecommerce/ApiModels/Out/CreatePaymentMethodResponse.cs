using Domain;
using Domain.PaymentMethodCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (paymentMethod.GetType() == typeof(CreditCard)) Bank = ((CreditCard)paymentMethod).Flag;
        }
    }
}
