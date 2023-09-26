using LogicInterface;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PurchaseController
    {
        private IPurchaseLogic _purchaseLogic;

        public PurchaseController(IPurchaseLogic purchaseLogic)
        {
            _purchaseLogic = purchaseLogic;
        }

        public OkObjectResult CreatePurchase(object purchaseRequest)
        {
            throw new NotImplementedException();
        }
    }
}