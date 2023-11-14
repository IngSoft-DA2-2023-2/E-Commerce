namespace Domain.PaymentMethodCategories
{
    public class Paypal : PaymentMethod
    {
        public override Guid Id { get; set; }
        public override string CategoryName { get; set; }
    }
}