using Domain;


namespace WebApi.Models.Out
{
    public class CreatePurchaseResponse
    {
        public Guid Id;
        public Guid BuyerId;
        public List<CreateProductResponse> Cart = new();

        public CreatePurchaseResponse(Purchase savedPurchase)
        {
            Id = savedPurchase.Id;
            BuyerId = savedPurchase.BuyerId;
            foreach(Product p in savedPurchase.Cart)
            {
                Cart.Add(new CreateProductResponse(p));
            }
        }
    }
}