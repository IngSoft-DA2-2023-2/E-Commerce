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
                foreach (Colour colour in newProduct.Colours) _colourLogic.CheckForColour(colour);
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

        public IEnumerable<Product> FilterUnionProduct(string? name, string? brandName, string? categoryName)
        {
            try
            {
                IEnumerable<Product> products = new List<Product>();
                if (name is not null) products = _productRepository.GetProductByName(name);
                if (brandName is not null)
                {
                    IEnumerable<Product> brandFilter = _productRepository.GetProductByBrand(brandName);
                    products = products.Union(brandFilter);
                }
                if (brandName is not null)
                {
                    IEnumerable<Product> categoryFilter = _productRepository.GetProductByCategory(categoryName);
                    products = products.Union(categoryFilter);
                }
                return products.Distinct();
            }catch(DataAccessException e)
            {
                throw new LogicException(e);
            }
            

        }

        public Product UpdateProduct(Product newProduct)
        {
            try
            {
                _brandLogic.CheckBrand(newProduct.Brand);
                _categoryLogic.CheckForCategory(newProduct.Category);
                foreach (Colour colour in newProduct.Colours) _colourLogic.CheckForColour(colour);
                return _productRepository.UpdateProduct(newProduct);

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public IEnumerable<Product> FilterIntersectionProduct(string? name, string? brandName, string? categoryName)
        {
            IEnumerable<Product> products = null;
            try
            {
                if (name is not null)
                {
                    products = _productRepository.GetProductByName(name);

                }
                if (brandName is not null)
                {
                    if(name is not null)
                    {
                        products = products.Intersect(_productRepository.GetProductByBrand(brandName));

                    }
                    else
                    {
                        products = _productRepository.GetProductByBrand(brandName);

                    }

                }
                if (categoryName is not null)
                {
                    if(brandName is not null)
                    {
                        products = products.Intersect(_productRepository.GetProductByCategory(categoryName));

                    }
                    else
                    {
                        products = _productRepository.GetProductByCategory(categoryName);

                    }

                }
            }
            catch(DataAccessException e)
            {
                throw new LogicException(e);
            }
            if (products.Count() <= 0) throw new LogicException("there is no product with those conditions");
            return products;
        }
    }
}
