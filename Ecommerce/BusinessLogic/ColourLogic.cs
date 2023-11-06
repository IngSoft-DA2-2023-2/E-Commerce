using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class ColourLogic : IColourLogic
    {
        private readonly IColourRepository _context;

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

        public IEnumerable<Colour> GetColours()
        {
            try
            {
                return _context.GetColours();
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}