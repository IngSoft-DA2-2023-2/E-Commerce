using Domain;

namespace LogicInterface
{
    public interface IUserLogic
    {
        public IEnumerable<User> GetUsers();
        public Guid AddUser(User user);
        public void DeleteUser(Guid id);

    }
}
