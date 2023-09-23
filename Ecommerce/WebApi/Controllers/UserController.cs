using ApiModels;
using ApiModels.UserRequest;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult GetAllUsers()
        {
            return Ok(_userLogic.GetAllUsers("").Select(u => new UserResponse(u)).ToList());
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserRequest received)
        {

            var user = received.ToEntity();
            var resultLogic = _userLogic.CreateUser(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(CreateUser), result);

        }

        [HttpDelete]
        public IActionResult DeleteUser([FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.DeleteUser(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(Guid id, [FromBody] UserRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.UpdateUser(id, user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

    }
}

