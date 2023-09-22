using DataAccessInterface;
using Domain;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ProductLogic : IProductLogic
    {
       private IProductRepository _productRepository;
        public ProductLogic(IProductRepository productRepository) 
        {
            _productRepository = productRepository;  
        }
        public Product AddProduct(Product newProduct)
        {
           Guid guid = Guid.NewGuid();
           newProduct.Id = guid;
           _productRepository.CreateProduct(newProduct);
           return newProduct;
        }

        public List<Product> GetProducts(string? name, string? brandName, string? categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
