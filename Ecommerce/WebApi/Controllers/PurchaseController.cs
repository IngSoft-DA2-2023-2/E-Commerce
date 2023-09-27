using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class PurchaseController :ControllerBase
    {
        private IPurchaseLogic _purchaseLogic;

        public PurchaseController(IPurchaseLogic purchaseLogic)
        {
            _purchaseLogic = purchaseLogic;
        }

        [HttpPost]
        [AnnotatedCustomExceptionFilter]
        [AuthenticationFilter]
        public IActionResult CreatePurchase([FromBody] CreatePurchaseRequest purchase)
        {
            var newpurchase = purchase.ToEntity();
            Purchase savedPurchase = _purchaseLogic.CreatePurchase(newpurchase);
            var response = new CreatePurchaseResponse(savedPurchase);
            return Ok(response);
        }
    }
}