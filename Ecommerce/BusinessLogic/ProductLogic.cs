using BusinessLogic.Promotions;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using System.Linq;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
       private IProductRepository _productRepository;
       private BrandLogic _brandLogic;
       private CategoryLogic _categoryLogic;
       private ColourLogic _colourLogic;
        public ProductLogic(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, IColourRepository colourRepository) 
        {
            _productRepository = productRepository; 
            _brandLogic = new BrandLogic(brandRepository);
            _categoryLogic = new CategoryLogic(categoryRepository);
            _colourLogic = new ColourLogic(colourRepository);
        }
        public Product AddProduct(Product newProduct)
        {
            try
            {
                Guid guid = Guid.NewGuid();
                newProduct.Id = guid;
                _brandLogic.CheckBrand(newProduct.Brand);
                _categoryLogic.CheckForCategory(newProduct.Category);
                foreach (Colour colour in newProduct.Colors) _colourLogic.CheckForColour(colour);
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

        public IEnumerable<Product> GetProducts(string? name, string? brandName, string? categoryName)
        {
            throw new NotImplementedException();
          
        }

        public Product UpdateProduct(Product newProduct)
        {
            try
            {
                _brandLogic.CheckBrand(newProduct.Brand);
                _categoryLogic.CheckForCategory(newProduct.Category);
                foreach (Colour colour in newProduct.Colors) _colourLogic.CheckForColour(colour);
                return _productRepository.UpdateProduct(newProduct);

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}
