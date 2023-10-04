using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;
        private readonly IUserLogic _userLogic;
        public SessionController(ISessionLogic logic, IUserLogic userLogic)
        {
            _sessionLogic = logic;
            _userLogic = userLogic;
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        public IActionResult LogIn([FromBody] CreateSessionRequest received)
        {
            Session session = _sessionLogic.LogIn(received.Email, received.Password);
            var result = new SessionResponse(session);

            return CreatedAtAction(nameof(LogIn), result);
        }

        [HttpDelete]
        [AnnotatedCustomExceptionFilter]
        public IActionResult LogOut([FromHeader] string Authorization)
        {
            Guid tokenUser = Guid.Parse(Authorization);
            Session session = _sessionLogic.LogOut(tokenUser);
            var result = new SessionResponse(session);

            return Ok(result);



        }
    }
}

