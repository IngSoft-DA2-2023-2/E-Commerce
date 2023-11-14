namespace Domain.ProductParts
{
    public class Brand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Equals(object? other)
        {
            return Name == ((Brand)other).Name;
        }
    }
}