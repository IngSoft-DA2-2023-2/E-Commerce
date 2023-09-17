
using System.Linq;
using Domain;

namespace LogicInterface

{
    public interface IProductLogic
    {
        public List<Product> GetProducts(string? name);

    }
}
