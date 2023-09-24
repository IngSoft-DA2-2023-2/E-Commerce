using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        IEnumerable<User> GetAllUsers(string emailOrEmpty);
        User CreateUser(User user);
        User DeleteUser(User user);
        User UpdateUser(User user);
    }
}
