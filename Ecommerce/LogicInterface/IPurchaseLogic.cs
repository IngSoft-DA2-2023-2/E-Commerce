using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace LogicInterface
{
    public interface IPurchaseLogic
    {
        public bool IsEligibleForPromotions(Purchase purchase);
        public void AssignsBestPromotion(Purchase purchase);
    }
}
