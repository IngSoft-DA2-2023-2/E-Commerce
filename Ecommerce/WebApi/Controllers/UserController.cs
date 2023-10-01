using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLogic _userLogic;
        public UserController(IUserLogic logic): base()
        {
            _userLogic = logic;
        }

        [HttpGet("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetUsersById([FromRoute] Guid id, [FromHeader] string Authorization)
        {

            var userHeader = Authorization;
            if (_userLogic.IsAdmin(userHeader))
            {
                return Ok(_userLogic.GetAllUsers(c => c.Id == id).Select(u => new UserResponse(u)).ToList());
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetUsers([FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (_userLogic.IsAdmin(userHeader))
            {
                return Ok(_userLogic.GetAllUsers(null).Select(u => new UserResponse(u)).ToList());
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


        }

        [HttpPost]
        public IActionResult SelfRegistration([FromBody] CreateUserByThemselfRequest received)
        {
            

            var user = received.ToEntity();
            var resultLogic = _userLogic.AddUserByThemself(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(RegistrationByAdmin), result);



        }

        [HttpPost]
        [Route("admin")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult RegistrationByAdmin([FromBody] CreateUserByAdminRequest received, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;

            if (_userLogic.IsAdmin(userHeader))
            {
                var user = received.ToEntity();
                var resultLogic = _userLogic.AddUserByAdmin(user);
                var result = new UserResponse(resultLogic);

                return CreatedAtAction(nameof(RegistrationByAdmin), result);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


            
        }

        [HttpDelete("{id}")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult DeleteUser([FromRoute] Guid id, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (_userLogic.IsAdmin(userHeader))
            {
                var resultLogic = _userLogic.DeleteUser(id);
                var result = new UserResponse(resultLogic);

                return Ok(result);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            
        }

        [HttpPut("admin")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUserByAdmin([FromBody] UpdateUserRequestByAdmin received, [FromRoute] Guid id, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (_userLogic.IsAdmin(userHeader))
            {
                var user = UserRequestByAdminToEntity(received);
                user.Id = id;

                var resultLogic = _userLogic.UpdateUserByAdmin(user);
                var result = new UserResponse(resultLogic);

                return Ok(result);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
            
        }

        private User UserRequestByAdminToEntity([FromBody] UpdateUserRequestByAdmin received)
        {

            User ret = new User();
            if (received.Name is not null) ret.Name = received.Name;
            if (received.Address is not null) ret.Address = received.Address;
            if (received.Roles is not null)
            {
               foreach(string receivedRol in received.Roles) ret.Roles.Add(new StringWrapper() { Info = receivedRol });
            }
            if (received.Password is not null) ret.Password = received.Password;
            return ret;
        }

        [HttpPut]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUserByThemself([FromBody] UpdateUserRequestByThemself received, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
           
                var id = _userLogic.GetUserIdFromToken(userHeader);

                var user = UpdateUserRequestByThemselfToEntity(received, id);


                var resultLogic = _userLogic.UpdateUserByThemself(user);
                var result = new UserResponse(resultLogic);

                return Ok(result);
            
            
        }

        private User UpdateUserRequestByThemselfToEntity([FromBody] UpdateUserRequestByThemself received, [FromRoute] Guid id)
        {

            User ret = new User();
            ret.Id = id;
            if (received.Name is not null) ret.Name = received.Name;
            if (received.Password is not null) ret.Password = received.Password;
            if (received.Address is not null) ret.Address = received.Address;
            return ret;
        }
    }
}

