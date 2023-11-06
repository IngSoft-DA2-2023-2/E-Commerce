using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class BrandLogic : IBrandLogic
    {
        private readonly IBrandRepository _context;

        public BrandLogic(IBrandRepository context)
        {
            _context = context;
        }

        public bool CheckBrand(Brand brand)
        {
            try
            {
                _context.CheckForBrand(brand.Name);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public List<Brand> GetBrands()
        {
            try
            {
               return _context.GetBrands();
            }catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}