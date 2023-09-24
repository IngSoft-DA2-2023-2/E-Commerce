using ApiModels;
using ApiModels.UserRequest;
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
        public IActionResult GetAllUsers()
        {
            return Ok(_userLogic.GetAllUsers("").Select(u => new UserResponse(u)).ToList());
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        public IActionResult CreateUser([FromBody] UserRequest received)
        {

            var user = received.ToEntity();
            var resultLogic = _userLogic.AddUser(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(CreateUser), result);

        }

        [HttpDelete]
        [AnnotatedCustomExceptionFilter]
        public IActionResult DeleteUser([FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.DeleteUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [AnnotatedCustomExceptionFilter]
        public IActionResult UpdateUser([FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.UpdateUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

    }
}

