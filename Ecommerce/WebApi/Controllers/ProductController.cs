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
        public IActionResult CreateProduct([FromBody] CreateProductRequest product)
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

            Product savedProduct = productLogic.AddProduct(newProduct);

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

        [HttpPut("/{id}")]
        [AnnotatedCustomExceptionFilter]

        public IActionResult UpdateProduct([FromRoute] Guid id, [FromBody] UpdateProductRequest product)
        {

            Product newProduct = new Product()
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Brand = product.Brand,
                Category = product.Category,
                Color = product.Color
            };
            var savedProduct = productLogic.UpdateProduct(newProduct);

            UpdateProductResponse response = new UpdateProductResponse()
            {
                GUID = savedProduct.Id,
                Name = savedProduct.Name,
                Description = savedProduct.Description,
                Price = savedProduct.Price,
                Brand = savedProduct.Brand,
                Category = savedProduct.Category,
                Colors = savedProduct.Color

            };
            return Ok(response);


        }
    }


}

