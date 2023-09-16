
using System.Linq;
using WebApi.Domain;

namespace WebApi.LogicInterface

{
    public interface IProductLogic
    {
        public List<Product> GetProducts();

    }
}
