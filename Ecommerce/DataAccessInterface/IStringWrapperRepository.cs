using Domain.ProductParts;

namespace DataAccessInterface
{
    public interface IStringWrapperRepository
    {
        public IEnumerable<StringWrapper> GetRoles();
    }
}