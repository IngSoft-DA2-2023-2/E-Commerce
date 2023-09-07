using System.Collections.Generic;
using System.Linq;

namespace BackEnd
{
    public class Promotion3x2
    {
        private const int _minQuantity = 3;
        public bool IsApplicable(Purchase purchase)
        {
            List<string> uniqueCategories = purchase.Cart.Select(p => p.Category).Distinct().ToList();

            foreach (string category in uniqueCategories)
            {
                List<Product> productsInCategory = purchase.Cart.Where(p => p.Category == category).ToList();

                if (productsInCategory.Count >= _minQuantity)
                {
                    return true;
                }
            }

            return false;
        }

        public int CalculateDiscount(Purchase purchase)
        {
            if (!IsApplicable(purchase)) throw new BackEndException("Not applicable promotion");

            List<string> uniqueCategories = purchase.Cart.Select(p => p.Category).Distinct().ToList();

            int maxDiscount = 0;
            foreach (string category in uniqueCategories)
            {
                List<Product> productsInCategory = purchase.Cart.Where(p => p.Category == category).ToList();
                productsInCategory.Sort((a,b)=>b.Price-a.Price);

                if (productsInCategory.Count < _minQuantity) continue;
                if (productsInCategory.Last().Price > maxDiscount) maxDiscount = productsInCategory.Last().Price;


            }
            return maxDiscount;
        }

    }


}

