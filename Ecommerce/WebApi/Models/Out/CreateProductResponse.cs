namespace WebApi.Models.Out
{
    public class CreateProductResponse
    {
        public Guid GUID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

    }
}
