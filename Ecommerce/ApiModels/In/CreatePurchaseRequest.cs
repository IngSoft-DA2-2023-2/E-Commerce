using Domain;

namespace ApiModels.In
{
    public class CreatePurchaseRequest
    {
        public List<CreateProductForPurchase> Cart { get; set; }

        public CreatePaymentMethodRequest PaymentMethod { get; set; }

        public Purchase ToEntity(Guid Buyer)
        {
            List<Product> products = new List<Product>();
            foreach (CreateProductForPurchase createProductRequest in Cart)
            {
                products.Add(createProductRequest.ToEntity());
            }
            return new Purchase() { Cart = products, UserId = Buyer, PaymentMethod = PaymentMethod.ToEntity() };
        }
    }
}