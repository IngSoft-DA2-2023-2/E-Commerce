using DataAccess.Context;
using DataAccessInterface;

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
            return true;
        }
    }
}