using System;

namespace BackEnd
{
    public class Promotion3x1Fidelity : IPromotionable
    {
        public bool IsApplicable(Purchase purchase)
        {
            return false;
        }
        public int CalculateDiscount(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
