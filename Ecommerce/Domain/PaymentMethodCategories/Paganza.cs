﻿using System.Diagnostics.CodeAnalysis;

namespace Domain.PaymentMethodCategories
{
    [ExcludeFromCodeCoverage]
    public class Paganza : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }
    }
}