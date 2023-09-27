using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult LogIn([FromBody] CreateSessionRequest received)
        {
            Session session = _sessionLogic.LogIn(received.Email, received.Password);
            var result = new CreateSessionResponse(session);

            return CreatedAtAction(nameof(result), result);
        }
    }
}
