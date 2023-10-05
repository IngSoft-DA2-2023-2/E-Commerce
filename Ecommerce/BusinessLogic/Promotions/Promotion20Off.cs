using Domain;
using LogicInterface;
using LogicInterface.Exceptions;


namespace BusinessLogic.Promotions
{
    public class Promotion20Off : IPromotionable
    {

        private const decimal _twentyPercent = 0.2m;
        private const int _minCartSize = 2;
        public string Name { get; } = "20% Off";

        public bool IsApplicable(List<Product> cart)
        {
            return cart.Count >= _minCartSize;
        }

        public int CalculateDiscount(List<Product> cart)
        {
            if (!IsApplicable(cart))
            {
                throw new LogicException("Not applicable promotion");
            }

            decimal maxPrice = 0;
            foreach (Product item in cart)
            {
                if (item.Price > maxPrice)
                {
                    maxPrice = item.Price;
                }
            }

            return (int)Decimal.Round(_twentyPercent * maxPrice);
        }
        public override string ToString()
        {
            return Name;
        }


    }
}