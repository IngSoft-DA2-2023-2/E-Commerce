namespace Domain.ProductParts
{
    public class Colour
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(Object? other)
        {
            return Name == ((Colour)other).Name;
        }
    }
}