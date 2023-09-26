using Domain;
using WebApi.Models.In;

namespace ApiModels.In
{
    public class CreatePurchaseRequest
    {
        public Guid Id { get; set; }
        public List<CreateProductRequest> Cart { get; set; } 

        public Purchase ToEntity()
        {
            List<Product> products = new List<Product>();
            foreach (CreateProductRequest createProductRequest in Cart)
            {
                products.Add(createProductRequest.ToEntity());
            }
            return new Purchase() { Cart = products, BuyerId = Id};
        }
    }
}