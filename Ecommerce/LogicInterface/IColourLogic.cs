using Domain.ProductParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface
{
    public interface IColourLogic
    {
        public bool CheckForColour(Colour colour);
        public IEnumerable<Colour> GetColours();
    }
}