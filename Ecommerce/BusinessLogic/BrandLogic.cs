using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class BrandLogic : IBrandLogic
    {
        private readonly IBrandRepository _brandRepository;

        public BrandLogic(IBrandRepository context)
        {
            _brandRepository = context;
        }

        public bool CheckBrand(Brand brand)
        {
            try
            {
                _brandRepository.CheckForBrand(brand.Name);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public IEnumerable<string> GetBrands()
        {
            try
            {
                return _brandRepository.GetBrands().Select(b => b.Name).Distinct();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}