using DataAccessInterface;
using DataAccessInterface.Exceptions;
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
            try
            {
                if (_userRepository.GetAllUsers(GetUserByEmail(user.Email)).Any())
                {
                    throw new LogicException("Existing user with that email");
                }
                return _userRepository.CreateUser(user);
            }
             catch(DataAccessException e)
            {
                throw new LogicException(e);
            }

        }

        public IEnumerable<User> GetAllUsers(string emailOrEmpty)
        {
            try
            {
                return _userRepository.GetAllUsers(GetUserByEmail(emailOrEmpty));

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public User UpdateUser(User user)
        {
            try
            {
              return _userRepository.UpdateUser(user);

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public User DeleteUser(User user)
        {
            try
            {
                return _userRepository.DeleteUser(user);


            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }


        private Func<User, bool> GetUserByEmail(string email)
        {
            return (User u) => email == "" || u.Email == email;
        }

    }
}
