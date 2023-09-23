using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetUsers(string nameOrEmpty);
        User UpdateUser(Guid id, User user);
        public User DeleteUser(User user);
    }
}
