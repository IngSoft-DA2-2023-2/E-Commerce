using Domain;

namespace LogicInterface
{
    public interface IPurchaseLogic
    {
        public bool IsEligibleForPromotions(Purchase purchase);
        public void AssignsBestPromotion(Purchase purchase);
    }
}
