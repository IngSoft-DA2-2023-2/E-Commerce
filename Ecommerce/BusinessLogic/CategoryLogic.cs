using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class CategoryLogic
    {
        private readonly ICategoryRepository _context;

        public CategoryLogic(ICategoryRepository context)
        {
            _context = context;
        }

        public bool CheckForCategory(Category category)
        {
            try
            {
                _context.CheckForCategory(category.Name);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}