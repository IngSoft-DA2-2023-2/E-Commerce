using Microsoft.AspNetCore.Mvc;
using WebApi.Domain;
using WebApi.LogicInterface;

namespace WebApi.Controllers
{
    public class ProductController : ControllerBase
    {
        private IProductLogic productLogic;

        public ProductController(IProductLogic productLogic) {
           this.productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProducts()
        {
            return Ok(productLogic.GetProducts());
        }
    }
}
