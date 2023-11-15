using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class StringWrapperLogic : IStringWrapperLogic
    {
        private readonly IStringWrapperRepository _stringWrapperRepository;

        public StringWrapperLogic(IStringWrapperRepository stringWrapperRepository)
        {
            _stringWrapperRepository = stringWrapperRepository;
        }

        public IEnumerable<StringWrapper> GetRoles()
        {
            try
            {
                return _stringWrapperRepository.GetRoles();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}