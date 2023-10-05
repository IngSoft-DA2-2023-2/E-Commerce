using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface.Exceptions;

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
    }
}