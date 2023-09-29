using Domain.Exceptions;

namespace Domain
{
    public class Purchase
    {
       
        private List<Product> _cart = new List<Product>();
        private DateTime _date;
        public DateTime Date
        {
            get => _date;
        }
        public Guid Id { get; set; }

        public Purchase()
        {
            _date = DateTime.Now;
        }
        public Guid User { get; set; }

        public string CurrentPromotion { get; set; }
        public virtual List<Product> Cart
        {
            get => _cart;
            set
            {
                ValidateCart(value);
                _cart = value;
            }
        }


        public void DropPromotion()
        {
            CurrentPromotion = null;
        }


        private static void ValidateCart(List<Product> value)
        {
            if (value == null) throw new DomainException("Cart must not be null");
            if (value.Count == 0) throw new DomainException("Cart must not be empty");
        }

    }
}
