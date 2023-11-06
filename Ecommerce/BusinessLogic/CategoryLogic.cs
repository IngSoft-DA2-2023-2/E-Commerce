using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryLogic(ICategoryRepository context)
        {
            _categoryRepository = context;
        }

        public bool CheckForCategory(Category category)
        {
            try
            {
                _categoryRepository.CheckForCategory(category.Name);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }


        public IEnumerable<Category> GetCategory()
        {
            try
            {
                return _categoryRepository.GetCategories();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}