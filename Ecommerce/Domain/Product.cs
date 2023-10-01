using Domain.Exceptions;
using Domain.ProductParts;

namespace Domain
{
    public class Product
    {
        private string _name;
        private int _price;
        private string _description;
        public Guid Id { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Colour> Colours { get; set; } = new List<Colour>();

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DomainException("Name must not be null");
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
                    throw new DomainException("Price must not be negative");
                }
                _price = value;
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new DomainException("Description must not be null");
                }
                _description = value;
            }

        }
        public override bool Equals(object? obj)
        {
            return Id.Equals(((Product)obj).Id);
        }


    }
}
