using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return new Purchase() { Cart = products};
        }
    }
}
