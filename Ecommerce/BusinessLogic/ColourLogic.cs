using DataAccessInterface;
using Domain.ProductParts;

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
            return _context.CheckForColour(colour.Name);
        }
    }
}