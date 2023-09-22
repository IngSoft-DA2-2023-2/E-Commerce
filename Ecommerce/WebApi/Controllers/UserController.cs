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
/*
        [HttpDelete]
        public ActionResult<CreateUserResponse> DeleteUser([FromQuery] Guid userId)
        {
            try
            {
                _userLogic.DeleteUser(userId);
                return Ok();
            }
            catch (LogicException)
            {
                return BadRequest();
            }
            catch(Exception)
            {
                return StatusCode(500);
            }

        }
       */
    }
}

