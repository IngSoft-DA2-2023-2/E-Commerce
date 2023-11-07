using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
        private readonly IProductRepository _productRepository;
        private readonly BrandLogic _brandLogic;
        private readonly CategoryLogic _categoryLogic;
        private readonly ColourLogic _colourLogic;
        public ProductLogic(IProductRepository productRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository, IColourRepository colourRepository)
        {
            _productRepository = productRepository;
            _brandLogic = new BrandLogic(brandRepository);
            _categoryLogic = new CategoryLogic(categoryRepository);
            _colourLogic = new ColourLogic(colourRepository);
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

        public IEnumerable<Product> FilterUnionProduct(string? name, string? brandName, string? categoryName, string? priceRange)
        {
            try
            {
                IEnumerable<Product> products = new List<Product>();
                if (name is null && brandName is null && categoryName is null && priceRange is null)
                {
                    return _productRepository.GetAllProducts();
                }
                if (name is not null) products = _productRepository.GetProductByName(name);
                if (brandName is not null)
                {
                    IEnumerable<Product> brandFilter = _productRepository.GetProductByBrand(brandName);
                    products = products.Union(brandFilter);
                }
                if (categoryName is not null)
                {
                    IEnumerable<Product> categoryFilter = _productRepository.GetProductByCategory(categoryName);
                    products = products.Union(categoryFilter);
                }
                if (priceRange is not null)
                {
                    IEnumerable<Product> priceFilter = _productRepository.GetProductByPriceRange(priceRange);
                    products = products.Union(priceFilter);
                }
                return products.Distinct();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }


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

        public IEnumerable<Product> FilterIntersectionProduct(string? name, string? brandName, string? categoryName, string? priceRange)
        {
            IEnumerable<Product>? products = null;
            try
            {
                if (name is not null)
                {
                    products = _productRepository.GetProductByName(name);

                }

                if (brandName is not null && name is not null)
                {
                    products = products.Intersect(_productRepository.GetProductByBrand(brandName));

                }
                else if (brandName is not null)
                {
                    products = _productRepository.GetProductByBrand(brandName);

                }

                if (categoryName is not null && (brandName is not null || name is not null))
                {

                    products = products.Intersect(_productRepository.GetProductByCategory(categoryName));

                }
                else if (categoryName is not null)
                {
                    products = _productRepository.GetProductByCategory(categoryName);

                }
                if(priceRange is not null && (brandName is not null || name is not null || categoryName is not null))
                {
                    products = products.Intersect(_productRepository.GetProductByPriceRange(priceRange));
                }
                else if (priceRange is not null)
                {
                    products = _productRepository.GetProductByPriceRange(priceRange);

                }
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
            return products;
        }

        public bool CheckProduct(Product expected)
        {
            IEnumerable<Product> products = _productRepository.GetAllProducts();
            foreach (Product product in products)
            {
                if (product.Equals(expected)) return true;
            }
            throw new LogicException("Product Does not exists.");

        }
    }
}
