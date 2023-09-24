using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        IEnumerable<User> GetAllUsers(Func<User, bool> predicate);
        User UpdateUser(User user);
        User DeleteUser(User user);
    }
}
