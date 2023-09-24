using Domain;

namespace LogicInterface
{
    public interface IPromotionable
    {
        bool IsApplicable(List<Product> purchase);

        int CalculateDiscount(List<Product> purchase);
    }
}
