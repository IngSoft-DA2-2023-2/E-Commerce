using DataAccessInterface;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User CreateUser(User user)
        {

            if (_userRepository.Exist(GetUserByEmail(user.Email)))
            {
                throw new LogicException("Existing user with that email");
            }
            return _userRepository.CreateUser(user);
        }

        public IEnumerable<User> GetAllUsers(string emailOrEmpty)
        {
            return _userRepository.GetAllUsers(GetUserByEmail(emailOrEmpty));
        }

        public User UpdateUser(User user)
        {
           return _userRepository.UpdateUser(user);
        }

        public User DeleteUser(User user)
        {
            return _userRepository.DeleteUser(user);
        }


        private Func<User, bool> GetUserByEmail(string email)
        {
            return (User u) => email == "" || u.Email == email;
        }

    }
}
