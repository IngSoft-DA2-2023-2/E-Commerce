using Domain;

namespace DataAccessInterface
{
    public interface IUserRepository
    {
        public User CreateUser(User user);
        public IEnumerable<User> GetAllUsers(Func<User, bool> predicate);
        public User UpdateUser(User user);
        public User DeleteUser(Guid user);
    }
}
