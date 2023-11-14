namespace Domain.PaymentMethodCategories
{
    public class CreditCard : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }
        public string Flag { get; set; }
    }
}