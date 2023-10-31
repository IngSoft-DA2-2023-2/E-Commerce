using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethodCategories
{
    public class PaymentMethodEntity 
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string? Bank { get; set; }
        public string? Flag { get; set; }
    }
}
