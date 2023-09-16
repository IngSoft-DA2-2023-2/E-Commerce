using Microsoft.AspNetCore.Mvc;
using Domain;
using LogicInterface;

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
            try
            {
                return Ok(productLogic.GetProducts());
            } catch (Exception ex)
            {
                return StatusCode(500);
            }
           
        }
    }
}
