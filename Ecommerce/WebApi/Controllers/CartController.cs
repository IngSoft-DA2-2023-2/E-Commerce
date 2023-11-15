using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/cart/promotions")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IPurchaseLogic _purchaseLogic;

        public CartController(IPurchaseLogic purchaseLogic)
        {
            _purchaseLogic = purchaseLogic;
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        public IActionResult CreateCart([FromBody] CreateCartRequest cart)
        {
            var newpurchase = cart.ToEntity();
            Purchase savedPurchase = _purchaseLogic.CreatePurchaseLogic(newpurchase);
            var response = new CreateCartResponse(savedPurchase);
            return Ok(response);
        }
    }
}