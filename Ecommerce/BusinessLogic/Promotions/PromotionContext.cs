using Domain;
using LogicInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Promotions
{
    public class PromotionContext
    {
        List<IPromotionable> promotions;
        public PromotionContext()
        {
            promotions = new List<IPromotionable>();
            IPromotionable percentageOff = new Promotion20Off();
            IPromotionable fidelity = new Promotion3x1Fidelity();
            IPromotionable getOneFree = new Promotion3x2();
            IPromotionable totalLook = new PromotionTotalLook();
            promotions.Add(percentageOff);
            promotions.Add(fidelity);
            promotions.Add(getOneFree);
            promotions.Add(totalLook);
        }

        public bool IsEligibleForPromotions(List<Product> cart)
        {
            return promotions.Any(promotion => promotion.IsApplicable(cart));
        }
    }
}
