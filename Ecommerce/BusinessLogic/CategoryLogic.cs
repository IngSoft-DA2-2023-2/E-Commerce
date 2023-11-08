using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using System.Globalization;

namespace BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryLogic(ICategoryRepository context)
        {
            _categoryRepository = context;
        }

        public bool CheckForCategory(string category)
        {
            try
            {
                _categoryRepository.CheckForCategory(category);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }


        public IEnumerable<string> GetCategories()
        {
            try
            {
                return _categoryRepository.GetCategories().Select(c=>c.Name).Distinct();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}