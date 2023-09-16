namespace WebApi.Models.Out
{
    public class CreateProductResponse
    {
        public Guid GUID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }

    }
}
