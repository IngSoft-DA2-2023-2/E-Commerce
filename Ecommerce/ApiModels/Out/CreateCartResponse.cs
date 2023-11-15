using Domain;

namespace ApiModels.Out
{
    public class CreateCartResponse
    {
        public string SelectedPromotion { get; set; }
        public int Total { get; set; }
        public List<CreateProductResponse> Cart { get; set; } = new List<CreateProductResponse>();

        public CreateCartResponse(Purchase savedPurchase)
        {
            SelectedPromotion = savedPurchase.CurrentPromotion;
            foreach (Product p in savedPurchase.Cart)
            {
                Cart.Add(new CreateProductResponse(p));
            }
            Total = savedPurchase.Total;
        }
    }
}