using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public IEnumerable<User> GetAllUsers(Func<User,bool>? predicate);
        public User AddUserByAdmin(User user);
        public User AddUserByThemself(User user);
        public User DeleteUser(User user);
        public User UpdateUserByAdmin(User user);
        public User UpdateUserByThemself(User user);
    }
}
