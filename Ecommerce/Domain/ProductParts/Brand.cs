namespace Domain.ProductParts
{
    public class Brand
    {
        public string Name { get; set; }
        
        public bool Equals(Brand other)
        {
            return Name == other.Name;  
        }
    }
}