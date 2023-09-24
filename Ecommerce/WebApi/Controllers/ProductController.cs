using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic productLogic;

        public ProductController(IProductLogic productLogic)
        {
            this.productLogic = productLogic;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetAllProductsByFilters([FromQuery] string? name = null,
            [FromQuery] string? brandName = null, [FromQuery] string? categoryName = null)
        {
            try
            {
                return Ok(productLogic.GetProducts(name, brandName, categoryName));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        public ActionResult<CreateProductResponse> CreateProduct([FromBody] CreateProductRequest product)
        {
            try
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

                var savedProduct = productLogic.AddProduct(newProduct);

                CreateProductResponse response = new CreateProductResponse()
                {
                    Id = savedProduct.Id,
                    Name = savedProduct.Name,
                    Description = savedProduct.Description,
                    Price = savedProduct.Price,
                    Brand = savedProduct.Brand,
                    Category = savedProduct.Category,
                    Colors = savedProduct.Color

                };
                return Ok(response);

            }
            catch (LogicException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {

                return StatusCode(500);
            }


        }

    }
}
