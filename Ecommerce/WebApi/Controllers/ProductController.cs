using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
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
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetAllProductsByFilters([FromQuery] string? name = null,
            [FromQuery] string? brandName = null, [FromQuery] string? categoryName = null)
        {
            return Ok(productLogic.GetProducts(name, brandName, categoryName));
        }

        [HttpGet("{id}")]
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetProductById([FromRoute] Guid id)
        {
            return Ok(productLogic.GetProductById(id));
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreateProduct([FromBody] CreateProductRequest product)
        {
            var newProduct = product.ToEntity();
            Product savedProduct = productLogic.AddProduct(newProduct);
            var response = new CreateProductResponse(savedProduct);
            return Ok(response);



        }

        [HttpPut("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest product)
        {
            var newProduct = product.ToEntity(id);
            var savedProduct = productLogic.UpdateProduct(newProduct);
            var response = new UpdateProductResponse(savedProduct);
            
            return Ok(response);


        }
    }


}

