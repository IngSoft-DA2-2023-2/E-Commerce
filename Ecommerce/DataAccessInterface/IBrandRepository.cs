using Domain.ProductParts;

namespace DataAccessInterface
{
    public interface IBrandRepository
    {
        public bool CheckForBrand(string brandName);

        public IEnumerable<Brand> GetBrands();
    }
}