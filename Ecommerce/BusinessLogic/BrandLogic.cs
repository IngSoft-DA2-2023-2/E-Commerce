using DataAccessInterface;
using Domain.ProductParts;

namespace BusinessLogic
{
    public class BrandLogic
    {
        private IBrandRepository _context;

        public BrandLogic(IBrandRepository context)
        {
            _context = context;
        }

        public bool CheckBrand(Brand brand)
        {
            return _context.CheckForBrand(brand.Name);
        }
    }
}