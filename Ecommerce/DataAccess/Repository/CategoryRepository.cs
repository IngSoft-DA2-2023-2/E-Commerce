using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceContext _context;
        public CategoryRepository(ECommerceContext context)
        {
            _context = context;
        }

        public bool CheckForCategory(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Name.Equals(categoryName));
            if(category is null)throw new DataAccessException ($"Category {categoryName} does not exists");
            return true;
        }
    }
}