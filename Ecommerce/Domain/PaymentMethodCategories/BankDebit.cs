using System.Diagnostics.CodeAnalysis;

namespace Domain.PaymentMethodCategories
{
    [ExcludeFromCodeCoverage]
    public class BankDebit : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }
        public string Bank { get; set; }
    }
}
