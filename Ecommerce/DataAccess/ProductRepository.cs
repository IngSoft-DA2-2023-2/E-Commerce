using DataAccessInterface;
using Domain;

namespace DataAccess
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext _eCommerceContext;
        public ProductRepository(ECommerceContext context)
        {
            _eCommerceContext = context;
        }

       

        public Product CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}