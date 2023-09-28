namespace Domain.ProductParts
{
    public class Category
    {
       public string Name { get; set; }
        public bool Equals(Category other)
        {
            return Name == other.Name;
        }
    }
}