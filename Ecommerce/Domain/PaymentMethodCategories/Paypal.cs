using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.PaymentMethodCategories
{
    public class Paypal : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }
        public override PaymentMethodEntity ToEntity()
        {
            return new PaymentMethodEntity()
            {
                Id = Id,
                CategoryName = CategoryName,
                Bank = Bank,
            };
        }
    }
}
