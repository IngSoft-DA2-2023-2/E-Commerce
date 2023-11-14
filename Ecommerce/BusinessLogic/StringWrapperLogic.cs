using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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