using Domain;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    internal class ProductLogic : IProductLogic
    {
        public Guid AddProduct(Product newProduct)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProducts(string? name, string? brandName, string? categoryName)
        {
            throw new NotImplementedException();
        }
    }
}
