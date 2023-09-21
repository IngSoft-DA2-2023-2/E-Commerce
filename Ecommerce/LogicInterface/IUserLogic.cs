using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public List<User> GetUsers();
        public Guid AddUser(User user);

    }
}
