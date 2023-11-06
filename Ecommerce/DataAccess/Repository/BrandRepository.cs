using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;

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
            if (brand is null) throw new DataAccessException($"Brand {brandName} does not exists");
            return true;
        }

        public IEnumerable<Brand> GetBrands()
        {
             var brands = _context.Brands.ToList();
            List<Brand> brandsReturn = new List<Brand>();
            foreach (Brand brand in brands)
            {
                if (!(brandsReturn.Contains(brand)))
                {
                    brandsReturn.Add(brand);
                }
            }
            return brandsReturn;
        }
    }
}