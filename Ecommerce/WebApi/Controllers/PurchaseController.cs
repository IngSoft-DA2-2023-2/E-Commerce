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

       
    }
}