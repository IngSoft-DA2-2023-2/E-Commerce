using DataAccess.Context;
using DataAccessInterface;

namespace DataAccess.Repository
{
    public class ColourRepository : IColourRepository
    {
        private readonly ECommerceContext _context;
        public ColourRepository(ECommerceContext context) 
        {
            _context = context;
        }

        public bool CheckForColour(string colourName)
        {
            var colour = _context.Colours.FirstOrDefault(c => c.Name == colourName);
            if (colour is null) return false;
            return true;
        }
    }
}