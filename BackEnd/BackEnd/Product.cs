using System.Collections.Generic;

namespace BackEnd
{
    public class Product
    {
        private string _name;
        private int _price;
        private string _description;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new BackEndException("Name must not be null");
                }
                _name = value;
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (value < 0)
                {
                    throw new BackEndException("Price must not be negative");
                }
                _price = value;
            }
        }

        public string Description {
            get => _description;
            set{
                if (string.IsNullOrEmpty(value))
                {
                    throw new BackEndException("Description must not be null");
                }
                _description = value;
            }
                
        }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Color { get; set; } = new List<string>();

    }
}
