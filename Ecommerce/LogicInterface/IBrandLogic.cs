using Domain.ProductParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface
{
    public interface IBrandLogic
    {
        public List<Brand> GetBrands();
    }
}
