using Domain.ProductParts;

namespace DataAccessInterface
{
    public interface IColourRepository
    {
        public bool CheckForColour(string colourName);
        public IEnumerable<Colour> GetColours();
    }
}