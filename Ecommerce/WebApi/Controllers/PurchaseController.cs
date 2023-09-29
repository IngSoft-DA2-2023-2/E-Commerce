using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/Purchases")]
    [ApiController]
    public class PurchaseController :ControllerBase
    {
        private readonly IPurchaseLogic _purchaseLogic;
        private readonly IUserLogic _userLogic;
        private readonly ISessionLogic _sessionLogic;

        public PurchaseController(IPurchaseLogic purchaseLogic, IUserLogic userLogic, ISessionLogic sessionLogic)
        {
            _purchaseLogic = purchaseLogic;
            _userLogic = userLogic;
            _sessionLogic = sessionLogic;
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreatePurchase([FromBody] CreatePurchaseRequest purchase, [FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            if (_userLogic.IsBuyer(userHeader))
            {
                var newpurchase = purchase.ToEntity();
                Purchase savedPurchase = _purchaseLogic.CreatePurchase(newpurchase);
                var response = new CreatePurchaseResponse(savedPurchase);
                return Ok(response);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
           
        }

        [HttpGet]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult GetAllPurchases([FromHeader] string Authorization)
        {
            var userHeader = Authorization;
            var tokenUserPurchase = _userLogic.GetUserIdFromToken(userHeader);
            if (_userLogic.IsBuyer(userHeader) && (_sessionLogic.GetTokenFromUserId(tokenUserPurchase)).ToString().Equals(userHeader))
            {
                return Ok(_purchaseLogic.GetPurchase(tokenUserPurchase));
            }else if(_userLogic.IsAdmin(userHeader))
            {
                return Ok(_purchaseLogic.GetAllPurchases());
            }
            else
            {
                throw new UnauthorizedAccessException();
            }
        }
    }
}