using BusinessLogic.Promotions;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
       private IProductRepository _productRepository;
        private PromotionContext _promotionContext;
        public ProductLogic(IProductRepository productRepository) 
        {
            _productRepository = productRepository; 
            _promotionContext = new PromotionContext();
        }
        public Product AddProduct(Product newProduct)
        {
            try
            {

                Guid guid = Guid.NewGuid();
                newProduct.Id = guid;
                return _productRepository.CreateProduct(newProduct);
            }
            catch
            (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public Product GetProductById(Guid id)
        {
            try
            {
                return _productRepository.GetProductById(id);
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public List<Product> GetProducts(string? name, string? brandName, string? categoryName)
        {
            throw new NotImplementedException();
        }

        public Product UpdateProduct(Product newProduct)
        {
            try
            {
                return _productRepository.UpdateProduct(newProduct);

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}
