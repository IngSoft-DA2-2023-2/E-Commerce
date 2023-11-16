using Domain;
using Domain.PaymentMethodCategories;

namespace ApiModels.In
{
    public class CreateCartRequest
    {
        public List<CreateProductRequest> Cart { get; set; }
        public Purchase ToEntity()
        {
            List<Product> products = new List<Product>();
            foreach (CreateProductRequest createProductRequest in Cart)
            {
                products.Add(createProductRequest.ToEntity());
            }
            return new Purchase() { Cart = products, PaymentMethod = new Paypal() { CategoryName = "PayPal" } };
        }
    }
}