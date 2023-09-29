using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Session
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }

    }
}
