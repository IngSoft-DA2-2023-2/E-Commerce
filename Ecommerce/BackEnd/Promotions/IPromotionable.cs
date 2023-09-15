using System.Collections.Generic;

namespace BackEnd.Promotions
{
    public interface IPromotionable
    {
        bool IsApplicable(List<Product> purchase);

        int CalculateDiscount(List<Product> purchase);
    }
}
