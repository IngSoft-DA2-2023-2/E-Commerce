using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class PaymentMethod
    {
        public Guid Id { get; set; }
        public string Category { get; set; }

        public string? SubCategoryName { get; set; }
    }
}
