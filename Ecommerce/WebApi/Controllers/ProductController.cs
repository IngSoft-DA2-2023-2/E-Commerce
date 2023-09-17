using Microsoft.AspNetCore.Mvc;
using Domain;
using LogicInterface;
using WebApi.Models.In;
using WebApi.Models.Out;

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
        public ActionResult<List<Product>> GetAllProductsByFilters([FromQuery] string? name = null,
            [FromQuery] string? brandName = null, [FromQuery] string? categoryName = null)
        {
            try
            {
                return Ok(productLogic.GetProducts(name, brandName, categoryName));
            } catch (Exception)
            {
                return StatusCode(500);
            }
           
        }

        [HttpPost]
        public ActionResult<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest product)
        {
            Product newProduct = new Product()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                Category = product.Category,
                Color = product.Color
            };

            var GUID = productLogic.AddProduct(newProduct);

            CreateProductResponse response = new CreateProductResponse()
            {
                Id = GUID,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                Category = product.Category,
                Colors = product.Color

            };
            return Ok(response);
        }


    }
}
