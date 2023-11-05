namespace LogicInterface
{
    public interface IPaymentMethod
    {
        public int CalculateDiscount(int total, string categoryName);

    }
}
