using System;
using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class Promotion3x1Fidelity : IPromotionable
    {
        private const int _minQuantity = 3;
        public bool IsApplicable(Purchase purchase)
        {
            List<string> uniqueBrands = purchase.Cart.Select(p => p.Brand).Distinct().ToList();

            foreach (string brand in uniqueBrands)
            {
                List<Product> productsOfBrand = purchase.Cart.Where(p => p.Brand == brand).ToList();

                if (productsOfBrand.Count >= _minQuantity)
                {
                    return true;
                }
            }
            return false;
        }

        public int CalculateDiscount(Purchase purchase)
        {
            throw new NotImplementedException();
        }
    }
}
