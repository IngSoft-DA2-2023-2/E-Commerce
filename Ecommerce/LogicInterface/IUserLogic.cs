using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public bool IsAdmin(string token);
        public IEnumerable<User> GetAllUsers(Func<User,bool>? predicate);
        public User AddUserByAdmin(User user);
        public User AddUserByThemself(User user);
        public User DeleteUser(Guid user);
        public User UpdateUserByAdmin(User user);
        public User UpdateUserByThemself(User user);
        public Guid GetUserIdFromToken(string userHeader);
    }
}
