﻿using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Promotions
{
    public class PromotionContext
    {
        private readonly List<IPromotionable> _promotions;
        public PromotionContext()
        {
            _promotions = new List<IPromotionable>();
            IPromotionable percentageOff = new Promotion20Off();
            IPromotionable fidelity = new Promotion3x1Fidelity();
            IPromotionable getOneFree = new Promotion3x2();
            IPromotionable totalLook = new PromotionTotalLook();
            _promotions.Add(percentageOff);
            _promotions.Add(fidelity);
            _promotions.Add(getOneFree);
            _promotions.Add(totalLook);
        }

        public bool IsEligibleForPromotions(List<Product> cart)
        {
            return _promotions.Any(promotion => promotion.IsApplicable(cart));
        }
        public string GetBestPromotion(List<Product> cart)
        {
            string best = "";
            int maxDiscount = 0;
            foreach (IPromotionable promotion in _promotions)
            {
                if(promotion.IsApplicable(cart))
                {
                    int currentDiscount = promotion.CalculateDiscount(cart);
                    if (currentDiscount > maxDiscount)
                    {
                        best = promotion.ToString();
                        maxDiscount = currentDiscount;
                    }
                }
            }
            return best;
        }
    }
}