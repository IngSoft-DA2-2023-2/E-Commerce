using System.Collections.Generic;
using WebApi.Domain;

namespace WebApi.LogicInterface
{
    public interface IPromotionable
    {
        bool IsApplicable(List<Product> purchase);

        int CalculateDiscount(List<Product> purchase);
    }
}
