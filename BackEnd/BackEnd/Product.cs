using System.Collections.Generic;

namespace BackEnd
{
    public class Product
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if(value==null) throw new BackEndException("Name must not be null");
                _name = value;
            }
            }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }
        public List<string> Color { get; set; } = new List<string>();

    }
}
