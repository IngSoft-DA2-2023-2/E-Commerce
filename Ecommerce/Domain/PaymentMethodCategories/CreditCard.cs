using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethodCategories
{
    public class CreditCard : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }

        public string Flag { get; set; }

      
    }
}
