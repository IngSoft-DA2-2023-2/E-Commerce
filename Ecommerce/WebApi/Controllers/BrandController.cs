using ApiModels.In;
using ApiModels.Out;
using BusinessLogic;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/products/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandLogic brandLogic;

        public BrandController(IBrandLogic brandLogic)
        {
            this.brandLogic = brandLogic;
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetAllBrands()
        {
            return Ok(brandLogic.GetBrands());
        }
    }


}

