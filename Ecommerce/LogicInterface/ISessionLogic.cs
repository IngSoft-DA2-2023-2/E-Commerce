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
        public Session LogIn(string email, string password);
        public Session LogOut(Guid token);

        public Guid GetTokenFromUserId(Guid userId);
    }
}
