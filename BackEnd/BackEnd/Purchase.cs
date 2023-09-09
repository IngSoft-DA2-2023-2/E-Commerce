using System;
using System.Collections.Generic;


namespace BackEnd
{
    public class Purchase
    {
        private List<Product> _cart;
        private DateTime _date;

        public User User { get; set; }
        public IPromotionable Promotion { get; set; }
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

        private void ValidateDate(DateTime value)
        {
            if (value.CompareTo(DateTime.Now) > 0)
            {
                throw new BackEndException("Purchase Date must be before the current date");
            }
            _date = value;
        }

        private void ValidateCart(List<Product> value)
        {
            if (value == null) throw new BackEndException("Cart must not be null");
            if (value.Count == 0) throw new BackEndException("Cart must not be empty");
        }
    }
}
