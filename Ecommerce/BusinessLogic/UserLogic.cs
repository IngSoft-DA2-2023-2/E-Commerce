using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using System.Xml.XPath;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        public UserLogic(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public User AddUser(User user)
        {
            try
            {
                if (_userRepository.GetAllUsers(u=>u.Email == user.Email).Any())
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

        public IEnumerable<User> GetAllUsers(Func<User,bool>? predicate)
        {
            try
            {
                if (predicate == null)
                {
                    return _userRepository.GetAllUsers(u => true);
                }

                return _userRepository.GetAllUsers(predicate);

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

        private Func<User, bool> GetUserByGuid(Guid? guid)
        {
            return (User u) => guid == null || u.Guid == guid;
        }
    }
}
