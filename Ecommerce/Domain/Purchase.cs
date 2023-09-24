using Domain.Exceptions;

namespace Domain
{
    public class Purchase
    {
        private List<Product> _cart = new List<Product>();
        private DateTime _date;

        public User User { get; set; }

        public Promotion CurrentPromotion { get; set; }
        public List<Promotion> Promotions { get; set; }
        public List<Product> Cart
        {
            get => _cart;
            set
            {
                ValidateCart(value);
                _cart = value;
            }
        }

        public DateTime Date
        {
            get => _date;
            set
            {
                ValidateDate(value);
                _date = value;
            }
        }
        public void DropPromotion()
        {
            CurrentPromotion = null;
        }

        private void ValidateDate(DateTime value)
        {
            if (value.CompareTo(DateTime.Now) > 0)
            {
                throw new DomainException("Purchase Date must be before the current date");
            }
            _date = value;
        }

        private static void ValidateCart(List<Product> value)
        {
            if (value == null) throw new DomainException("Cart must not be null");
            if (value.Count == 0) throw new DomainException("Cart must not be empty");
        }

    }
}
