using System.Collections.Generic;

namespace BackEnd
{
    public class Promotion3x2
    {
        public bool IsApplicable(Purchase purchase)
        {
            List<string> categories = new List<string>();
            foreach (Product p in purchase.Cart)
            {
                if (!categories.Contains(p.Category)) categories.Add(p.Category);
            }

            foreach (string category in categories)
            {
                List<Product> prod = purchase.Cart.FindAll(x => x.Category == category);

                if (prod.Count < 3) continue;

                return true;
            }
            return false;
        }
    }
}
