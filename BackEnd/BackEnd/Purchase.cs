using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BackEnd
{
    public class Purchase
    {
        private List<Product> _cart;
        public User User { get; set; }
        public List<Product> Cart {
            get => _cart;

            set
            {
                ValidateCart(value);
                _cart = value;
            }
        }

        private static void ValidateCart(List<Product> value)
        {
            if (value == null) throw new BackEndException("Cart must not be empty");
        }

        public DateTime Date { get; set; }
        public Promotion Promotion { get; set; }
    }
}
