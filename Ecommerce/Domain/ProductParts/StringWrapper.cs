using System.Diagnostics.CodeAnalysis;

namespace Domain.ProductParts
{
    [ExcludeFromCodeCoverage]
    public class StringWrapper
    {
        public Guid Id { get; set; }
        public string Info { get; set; }
        public override bool Equals(object? obj)
        {
            return Info == ((StringWrapper)obj).Info;
        }
    }
}