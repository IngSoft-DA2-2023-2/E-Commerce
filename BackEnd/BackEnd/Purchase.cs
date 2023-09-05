using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Purchase
    {
        public User User { get; set; }
        public List<Product> Cart { get; set; }
        public DateTime Date { get; set; }
    }
}
