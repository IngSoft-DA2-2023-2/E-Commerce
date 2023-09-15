using System.Collections.Generic;
using BackEnd.Domain;

namespace BackEnd.LogicInterface
{
    public interface IPromotionable
    {
        bool IsApplicable(List<Product> purchase);

        int CalculateDiscount(List<Product> purchase);
    }
}
