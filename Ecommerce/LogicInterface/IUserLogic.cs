using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public IEnumerable<User> GetAllUsers(string emailOrEmpty);
        public User AddUser(User user);
        public User DeleteUser(User user);
        public User UpdateUser(User user);
    }
}
