using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Promotion
    {
        public string Name { get; set; }  
        public string Description { get; set; }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

    }
}
