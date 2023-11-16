using Domain.ProductParts;

namespace LogicInterface
{
    public interface IBrandLogic
    {
        public IEnumerable<string> GetBrands();
        public bool CheckBrand(Brand brand);
    }
}