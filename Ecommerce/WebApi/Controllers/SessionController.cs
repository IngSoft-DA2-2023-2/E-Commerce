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

            return CreatedAtAction(nameof(result), result);
        }

        [HttpDelete]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult LogOut([FromBody] DeleteSessionRequest request, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            var token = request.Token.ToString();
            if (token.Equals(userHeader))
            {
                var id = _userLogic.GetUserIdFromToken(userHeader);
                Session session = _sessionLogic.LogOut(request.Token);
                var result = new SessionResponse(session);

                return Ok(result);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }


        }
    }
}

