using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic logic)
        {
            _userLogic = logic;
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetUsers(Guid? guid)
        {
            if(guid == null)
                return Ok(_userLogic.GetAllUsers(null).Select(u => new UserResponse(u)).ToList());

            return Ok(_userLogic.GetAllUsers(c => c.Guid == guid).Select(u => new UserResponse(u)).ToList());
        }


        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreateUser([FromBody] UserRequest received)
        {

            var user = received.ToEntity();
            var resultLogic = _userLogic.AddUser(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(CreateUser), result);

        }

        [HttpDelete]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult DeleteUser([FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.DeleteUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUser([FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.UpdateUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

    }
}

