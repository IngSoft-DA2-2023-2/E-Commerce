using Domain;


namespace ApiModels.Out
{
    public class CreatePurchaseResponse
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string SelectedPromotion { get; set; }
        public int Total { get; set; }
        public CreatePaymentMethodResponse PaymentMethod {  get; set; }

        public List<CreateProductResponse> Cart { get; set; } = new List<CreateProductResponse>();
        public CreatePurchaseResponse(Purchase savedPurchase)
        {
            Id = savedPurchase.Id;
            BuyerId = savedPurchase.UserId;
            PurchaseTime = savedPurchase.Date;
            SelectedPromotion = savedPurchase.CurrentPromotion;
            foreach (Product p in savedPurchase.Cart)
            {
                Cart.Add(new CreateProductResponse(p));
            }
            Total = savedPurchase.Total;
            PaymentMethod = new CreatePaymentMethodResponse(savedPurchase.PaymentMethod);
        }
    }
}