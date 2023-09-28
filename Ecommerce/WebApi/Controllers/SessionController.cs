using ApiModels;
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
        public SessionController(ISessionLogic logic)
        {
            _sessionLogic = logic;
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult LogIn([FromBody] CreateSessionRequest received)
        {
            Session session = _sessionLogic.LogIn(received.Email, received.Password);
            var result = new SessionResponse(session);

            return CreatedAtAction(nameof(result), result);
        }

        [HttpDelete]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult LogOut([FromBody] DeleteSessionRequest request)
        {
            Session session = _sessionLogic.LogOut(request.Token);
            var result = new SessionResponse(session);

            return Ok(result);
        }
    }
}

