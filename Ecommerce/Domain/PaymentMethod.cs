using Domain.PaymentMethodCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class PaymentMethod
    {
        public abstract Guid Id { get; set; }
        public abstract string CategoryName { get; set; }

    }
}
