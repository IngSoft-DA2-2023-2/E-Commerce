using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;
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
        public IActionResult GetUsersById([FromRoute] Guid id)
        {
            return Ok(_userLogic.GetAllUsers(c => c.Guid == id).Select(u => new UserResponse(u)).ToList());
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetUsers()
        {
  
                return Ok(_userLogic.GetAllUsers(null).Select(u => new UserResponse(u)).ToList());

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
        public IActionResult RegistrationByAdmin([FromBody] CreateUserByAdminRequest received)
        {
            var user = received.ToEntity();
            var resultLogic = _userLogic.AddUserByAdmin(user);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(RegistrationByAdmin), result);
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

        [HttpPut("admin")]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUserByAdmin([FromBody] UpdateUserRequestByAdmin received,[FromQuery]Guid id)
        {
              var user = UserRequestByAdminToEntity(received);
              user.Guid = id;

              var resultLogic = _userLogic.UpdateUserByAdmin(user);
              var result = new UserResponse(resultLogic);

              return Ok(result);
        }

        private User UserRequestByAdminToEntity(UpdateUserRequestByAdmin received)
        {
            User ret = new User();
            if (received.Name is not null) ret.Name = received.Name;
            if (received.Address is not null) ret.Address = received.Address;
            if (received.Roles is not null) ret.Roles = received.Roles;
            if (received.Password is not null) ret.Password = received.Password;
            return ret;
        }

        [HttpPut]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult UpdateUserByThemself([FromBody] UpdateUserRequestByThemself received,[FromQuery]Guid id)
        {
            var user = UpdateUserRequestByThemselfToEntity(received,id);


            var resultLogic = _userLogic.UpdateUserByThemself(user);
            var result = new UserResponse(resultLogic);

            return Ok(result);
        }

        private User UpdateUserRequestByThemselfToEntity(UpdateUserRequestByThemself received,Guid id)
        {
            User ret = new User();
            ret.Guid=id;
            if (received.Name is not null) ret.Name = received.Name;
            if (received.Password is not null) ret.Password = received.Password;
            if (received.Address is not null) ret.Address = received.Address;
            return ret;
        }
    }
}

