using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public IEnumerable<User> GetUsers();
        public User CreateUser(User user);
        public User DeleteUser(User user);

    }
}
