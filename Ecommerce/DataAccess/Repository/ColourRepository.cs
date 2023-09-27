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
            return true;
        }
    }
}