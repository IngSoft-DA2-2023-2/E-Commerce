using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository
    {
        User CreateUser(User user);
        bool Exist(Func<User, bool> predicate);
        IEnumerable<User> GetUsers(Func<User, bool> predicate);
    }
}
