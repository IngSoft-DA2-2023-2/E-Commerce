using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        User DeleteUser(User user);
        bool Exist(Func<User, bool> predicate);
        IEnumerable<User> GetAllUsers(Func<User, bool> predicate);
        User UpdateUser(User user);
    }
}
