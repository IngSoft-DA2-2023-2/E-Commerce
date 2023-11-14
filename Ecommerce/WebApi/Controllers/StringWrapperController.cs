using ApiModels.In;
using ApiModels.Out;
using BusinessLogic;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/users/roles")]
    [ApiController]
    public class StringWrapperController : ControllerBase
    {
        private readonly IStringWrapperLogic _stringWrapperLogic;

        public StringWrapperController(IStringWrapperLogic stringWrapperLogic)
        {
            _stringWrapperLogic = stringWrapperLogic;
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        public IActionResult GetAllRoles()
        {
            return Ok(_stringWrapperLogic.GetRoles().Select(r => r.Info));
        }
    }
}