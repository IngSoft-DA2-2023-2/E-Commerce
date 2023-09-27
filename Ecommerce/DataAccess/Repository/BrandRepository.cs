using DataAccess.Context;
using DataAccessInterface;

namespace DataAccess.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ECommerceContext _context;
        public BrandRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForBrand(string brandName)
        {
            var brand = _context.Brands.FirstOrDefault(b => b.Name.Equals(brandName));
            if (brand is null) return false;
            return true;
        }
    }
}