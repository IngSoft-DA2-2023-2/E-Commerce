using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class ColourLogic
    {
        private IColourRepository _context;

        public ColourLogic(IColourRepository context)
        {
            _context = context;
        }

        public bool CheckForColour(Colour colour)
        {
            try
            {
                _context.CheckForColour(colour.Name);
                return true;
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }

        }
    }
}