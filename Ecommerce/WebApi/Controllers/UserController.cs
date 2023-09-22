using ApiModels;
using ApiModels.UserRequest;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                return Ok(_userLogic.GetUsers().Select(u => new UserResponse(u)).ToList());           
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
    }
}

