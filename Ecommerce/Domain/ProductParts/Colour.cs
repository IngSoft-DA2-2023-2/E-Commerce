namespace Domain.ProductParts
{
    public class Colour
    {
        public string Name { get; set; }

        public bool Equals(Colour other)
        {
            return Name == other.Name;
        }
    }
}