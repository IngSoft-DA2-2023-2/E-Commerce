namespace BackEnd
{
    public class Promotion20Off : IPromotionable
    {
        private const float _twentyPercent = 0.2f;
        private const int _minCartSize = 2;

        public bool IsApplicable(Purchase purchase)
        {
            return purchase.Cart.Count >= _minCartSize;
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase))
            {
                throw new BackEndException("Not applicable promotion");
            }

            int maxPrice = 0;
            foreach (Product item in purchase.Cart)
            {
                if (item.Price > maxPrice)
                {
                    maxPrice = item.Price;
                }
            }

            return (int)(_twentyPercent * maxPrice);
        }


    }
}