using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductLogic productLogic;
        private readonly IUserLogic userLogic;

        public ProductController(IProductLogic productLogic, IUserLogic userLogic)
        {
            this.productLogic = productLogic;
            this.userLogic = userLogic;
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetAllProductsByFilters([FromQuery] string? operation, [FromQuery] string? name = null,
            [FromQuery] string? brandName = null, [FromQuery] string? categoryName = null, [FromQuery] string? priceRange = null)
        {
            if (operation is null || operation == "or") return Ok(productLogic.FilterUnionProduct(name, brandName, categoryName, priceRange));
            if (operation == "and") return Ok(productLogic.FilterIntersectionProduct(name, brandName, categoryName,priceRange));
            return BadRequest();
        }

        [HttpGet("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetProductById([FromRoute] Guid id)
        {
            return Ok(productLogic.GetProductById(id));
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreateProduct([FromBody] CreateProductRequest product, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (userLogic.IsAdmin(userHeader))
            {
                var newProduct = product.ToEntity();
                Product savedProduct = productLogic.AddProduct(newProduct);
                var response = new CreateProductResponse(savedProduct);
                return Ok(response);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


        }

        [HttpPut("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest product, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (userLogic.IsAdmin(userHeader))
            {
                var newProduct = product.ToEntity(id);
                var savedProduct = productLogic.UpdateProduct(newProduct);
                var response = new UpdateProductResponse(savedProduct);

                return Ok(response);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

        }
    }


}

