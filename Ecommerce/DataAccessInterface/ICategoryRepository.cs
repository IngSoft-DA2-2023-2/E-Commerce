using Domain.ProductParts;

namespace DataAccessInterface
{
    public interface ICategoryRepository
    {
        public bool CheckForCategory(string categoryName);
        public IEnumerable<Category> GetCategories();
    }
}