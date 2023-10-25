using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethodCategories
{
    public class BankDebit : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set ; }

        public string Bank { get; set; }
    }
}
