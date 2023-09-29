using Domain;


namespace ApiModels.Out
{
    public class CreatePurchaseResponse
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string SelectedPromotion { get; set; }
        public List<CreateProductResponse> Cart { get; set; } = new List<CreateProductResponse>();
        public CreatePurchaseResponse(Purchase savedPurchase)
        {
            Id = savedPurchase.Id;
            BuyerId = savedPurchase.User;
            PurchaseTime = savedPurchase.Date;
            SelectedPromotion = savedPurchase.CurrentPromotion; 
            foreach(Product p in savedPurchase.Cart)
            {
                Cart.Add(new CreateProductResponse(p));
            }
        }
    }
}