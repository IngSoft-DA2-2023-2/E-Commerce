using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class Purchase
    {
        private List<Product> _cart;
        private DateTime _date;

        public User User { get; set; }
        public IPromotionable CurrentPromotion { get; private set; }
        public List<IPromotionable> Promotions { get; set; }
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

        public bool IsEligibleForPromotions()
        {
            return Promotions.Any(promo => promo.IsApplicable(Cart));
        }

        public void AssignsBestPromotion()
        {
            if (!IsEligibleForPromotions()) throw new BackEndException("Not eligible for promotions");
            IPromotionable best = null;
            int maxDiscount = 0;
            foreach (var promo in Promotions)
            {
                if (promo.IsApplicable(Cart))
                {
                    int currentDiscount = promo.CalculateDiscount(Cart);
                    if (currentDiscount > maxDiscount)
                    {
                        best = promo;
                        maxDiscount = currentDiscount;
                    }
                }
            }

            CurrentPromotion = best;
        }

        public void DropPromotion()
        {
            CurrentPromotion = null;
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
