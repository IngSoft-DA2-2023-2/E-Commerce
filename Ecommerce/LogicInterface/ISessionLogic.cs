using Domain;

namespace LogicInterface
{
    public interface ISessionLogic
    {
        public Session LogIn(string email, string password);
        public Session LogOut(Guid token);

    }
}
