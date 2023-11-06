using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;

namespace DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ECommerceContext _categoryRepository;
        public CategoryRepository(ECommerceContext context)
        {
            _categoryRepository = context;
        }

        public bool CheckForCategory(string categoryName)
        {
            var category = _categoryRepository.Categories.FirstOrDefault(c => c.Name.Equals(categoryName));
            if (category is null) throw new DataAccessException($"Category {categoryName} does not exists");
            return true;
        }

        public IEnumerable<Category> GetCategories()
        {

            var categories = _categoryRepository.Categories.ToList();
            List<Category> categoriesReturn = new List<Category>();
            foreach (Category category in categories)
            {
                if (!(categoriesReturn.Contains(category)))
                {
                    categoriesReturn.Add(category);
                }
            }
            return categoriesReturn;
        }
    }
}