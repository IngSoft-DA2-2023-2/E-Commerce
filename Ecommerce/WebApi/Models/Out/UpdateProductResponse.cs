namespace WebApi.Models.Out
{
    public class UpdateProductResponse
    {
        public Guid GUID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

    }
}
