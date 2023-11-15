using System.Diagnostics.CodeAnalysis;

namespace Domain
{
    [ExcludeFromCodeCoverage]
    public abstract class PaymentMethod
    {
        public abstract Guid Id { get; set; }
        public abstract string CategoryName { get; set; }
    }
}