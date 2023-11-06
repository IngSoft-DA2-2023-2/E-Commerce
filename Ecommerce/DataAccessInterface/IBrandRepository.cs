using Domain.ProductParts;

namespace DataAccessInterface
{
    public interface IBrandRepository
    {
        public bool CheckForBrand(string brandName);

        public List<Brand> GetBrands();
    }
}