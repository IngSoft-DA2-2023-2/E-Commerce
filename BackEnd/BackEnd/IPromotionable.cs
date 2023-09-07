namespace BackEnd
{
    public interface IPromotionable
    {
        bool IsApplicable(Purchase purchase);

        int CalculateDiscount(Purchase purchase);
    }
}
