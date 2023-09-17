using Microsoft.AspNetCore.Mvc;
using Domain;
using LogicInterface;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductLogic productLogic;

        public ProductController(IProductLogic productLogic) {
           this.productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProductsByFilters([FromQuery] string? name = null)
        {
            try
            {
                return Ok(productLogic.GetProducts(name));
            } catch (Exception ex)
            {
                return StatusCode(500);
            }
           
        }


    }
}
