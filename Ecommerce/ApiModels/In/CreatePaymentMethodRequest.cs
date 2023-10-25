using Domain;
using Domain.PaymentMethodCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.In
{
    public class CreatePaymentMethodRequest
    {
        public string CategoryName { get; set; }
        public string? Bank { get; set; }
        public string? Flag { get; set; }

        public PaymentMethod ToEntity()
        {
            switch (CategoryName) { 
                case "CreditCard":
                return new CreditCard
                {
                    Flag = Flag
                };
            case "BankDebit":
                return new BankDebit
                {
                    Bank = Bank
                };
            case "Paganza":
                return new Paganza();
            case "PayPal":
                return new Paypal();
            default:
                throw new Exception("Invalid Payment Method Category");
            }
        }
    }
}
