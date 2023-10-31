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
                    CategoryName = CategoryName,
                    Flag = Flag
                };
                case "BankDebit":
                    return new BankDebit
                    {
                        CategoryName = CategoryName,
                        Bank = Bank
                    };
                case "Paganza":
                    return new Paganza()
                    {
                        CategoryName = CategoryName,
                    };
                case "PayPal":
                    return new Paypal()
                    {
                        CategoryName = CategoryName,
                    };
                default:
                    throw new Exception("Invalid Payment Method Category");
                }
        }
    }
}
