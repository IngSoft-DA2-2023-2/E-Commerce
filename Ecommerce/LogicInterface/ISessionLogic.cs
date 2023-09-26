using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterface
{
    public interface ISessionLogic
    {
        Session LogIn(string email, string password);
    }
}
