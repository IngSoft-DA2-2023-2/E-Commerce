using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic logic)
        {
            _userLogic = logic;
        }

        [HttpGet("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetUsers(Guid? id)
        {
            if(id == null)
                return Ok(_userLogic.GetAllUsers(null).Select(u => new UserResponse(u)).ToList());

            return Ok(_userLogic.GetAllUsers(c => c.Guid == id).Select(u => new UserResponse(u)).ToList());
        }


        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreateUser([FromBody] CreateUserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.AddUser(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(CreateUser), result);

        }

        [HttpDelete("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult DeleteUser(Guid id)
        {
            var user = _userLogic.GetAllUsers(u=>u.Guid == id).FirstOrDefault();
            
            var resultLogic = _userLogic.DeleteUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

        [HttpPut("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUser([FromBody] UpdateUserRequest received,Guid id)
        {
            var user = received.ToEntity();
            user.Guid = id;

            var resultLogic = _userLogic.UpdateUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

    }
}

