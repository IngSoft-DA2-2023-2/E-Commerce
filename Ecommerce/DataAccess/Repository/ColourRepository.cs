using DataAccess.Context;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain.ProductParts;

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
            if (colour is null) throw new DataAccessException($"Colour {colourName} does not exists");
            return true;
        }

        public IEnumerable<Colour> GetColours()
        {
            var colours = _context.Colours.ToList();
            List<Colour> coloursReturn = new List<Colour>();
            foreach (Colour colour in colours)
            {
                if (!(coloursReturn.Contains(colour)))
                {
                    coloursReturn.Add(colour);
                }
            }
            return coloursReturn;
        }
    }
}