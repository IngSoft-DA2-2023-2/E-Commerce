using ApiModels.In;
using ApiModels.Out;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionLogic _sessionLogic;
        public SessionController(ISessionLogic logic)
        {
            _sessionLogic = logic;
        }

        [HttpPost]
        public IActionResult LogIn([FromBody] CreateSessionRequest received)
        {
            var resultLogic = _sessionLogic.LogIn(received.Email,received.Password);
            var result = new UserResponse(resultLogic);

            return CreatedAtAction(nameof(RegistrationByAdmin), result);
        }
    }
}
