using DataAccessInterface;
using Domain.ProductParts;

namespace BusinessLogic
{
    public class CategoryLogic
    {
        private ICategoryRepository _context;

        public CategoryLogic(ICategoryRepository context)
        {
           _context = context;
        }

        public bool CheckForCategory(Category category)
        {
            return _context.CheckForCategory(category.Name);
        }
    }
}