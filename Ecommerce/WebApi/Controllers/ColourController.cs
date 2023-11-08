using ApiModels.In;
using ApiModels.Out;
using BusinessLogic;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/products/colours")]
    [ApiController]
    public class ColourController : ControllerBase
    {
        private readonly IColourLogic _colourLogic;

        public ColourController(IColourLogic colourLogic)
        {
            _colourLogic = colourLogic;
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetAllColours()
        {
            return Ok(_colourLogic.GetColours().Select(c=>c.Name));
        }
    }


}

