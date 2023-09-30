namespace Domain.ProductParts
{
    public class Category
    {
       public Guid Id { get; set; }
       public string Name { get; set; }
        public bool Equals(object? other)
        {
            return Name == ((Category)other).Name;
        }
    }
}