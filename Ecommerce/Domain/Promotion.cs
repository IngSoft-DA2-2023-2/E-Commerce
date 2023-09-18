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
            var other = obj as Promotion;
            if (other == null) return false;
            if (other.Name != Name) return false;
            return true;
        }

    }
}
