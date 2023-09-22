using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models.In;
using WebApi.Models.Out;

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
        public ActionResult<List<User>> GetAllUsers()
        {
            try
            {
                return Ok(_userLogic.GetUsers());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public ActionResult<CreateUserResponse> CreateUser([FromBody] CreateUserRequest user)
        {
            try
            {
                User newUser = new()
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Address = user.Address,
                    Roles = user.Roles,
                };

                var GUID = _userLogic.AddUser(newUser);

                CreateUserResponse response = new()
                {
                    Id = GUID,
                    Name = user.Name,
                    Email = user.Email,
                    Address = user.Address,
                    Roles = user.Roles,
                    Password = user.Password,
                };

                return Ok(response);
            }
            catch (LogicException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

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

    }
}

