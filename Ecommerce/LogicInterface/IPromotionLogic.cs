using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace LogicInterface
{
    public interface IPromotionLogic
    {
        public IPromotionable GetPromotionable(Promotion promotion);
    }
}
