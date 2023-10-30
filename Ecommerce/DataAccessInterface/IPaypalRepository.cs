using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface IPaypalRepository
    {
       public bool CheckForPaypal(string paypal);
    }
}
