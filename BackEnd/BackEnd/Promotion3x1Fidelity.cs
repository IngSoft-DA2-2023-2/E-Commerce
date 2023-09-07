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
            if (!IsApplicable(purchase)) 
            {
                throw new BackEndException("Not applicable promotion");
            }

            int currentDiscount = 0;

            List<string> uniqueBrands = purchase.Cart.Select(p => p.Brand).Distinct().ToList();

            foreach (string brand in uniqueBrands)
            {
                List<Product> productsOfBrand = purchase.Cart.Where(p => p.Brand == brand).ToList();

                if (productsOfBrand.Count >= _minQuantity)
                {
                    productsOfBrand.Sort((a,b)=> a.Price.CompareTo(b.Price));
                    if(productsOfBrand[0].Price+ productsOfBrand[1].Price>currentDiscount)
                    {
                        currentDiscount = productsOfBrand[0].Price + productsOfBrand[1].Price;
                    }
                }
            }
            return currentDiscount;
        }

    }
}

