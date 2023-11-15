using Domain.ProductParts;

namespace LogicInterface
{
    public interface IColourLogic
    {
        public bool CheckForColour(Colour colour);
        public IEnumerable<Colour> GetColours();
    }
}