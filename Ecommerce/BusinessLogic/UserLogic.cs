using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
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

        public User AddUserByAdmin(User user)
        {
            try
            {
                if (_userRepository.GetAllUsers(u=>u.Email == user.Email).Any())
                {
                    throw new LogicException("Existing user with that email");
                }
                user.Id = Guid.NewGuid();
                return _userRepository.CreateUser(user);
            }
             catch(DataAccessException e)
            {
                throw new LogicException(e);
            }

        }

        public User AddUserByThemself(User user)
        {    
            try
            {
                if (_userRepository.GetAllUsers(u => u.Email == user.Email).Any())
                {
                    throw new LogicException("Existing user with that email");
                }
                user.Roles = new List<StringWrapper> {new StringWrapper(){Info= "buyer" } };
                user.Id = Guid.NewGuid();
                return _userRepository.CreateUser(user);
            }
            catch (DataAccessException e)
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


        public User UpdateUserByAdmin(User updated)
        {
            try
            {
                var outdated = _userRepository.GetAllUsers(u => u.Id == updated.Id).FirstOrDefault();
                    if (outdated == null) throw new LogicException("User not found");
                
                    if (updated.Address != null) outdated.Address = updated.Address;
                    if (updated.Password != null) outdated.Password = updated.Password;
                    if (outdated.Roles != null ) outdated.Roles = updated.Roles;
                    if (updated.Name != null) outdated.Name = updated.Name;
                    if(updated.Email != null) outdated.Email = updated.Email;
                
              return _userRepository.UpdateUser(outdated);

            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public User UpdateUserByThemself(User updated)
        {
            try
            {
                var outdated = _userRepository.GetAllUsers(u => u.Id == updated.Id).FirstOrDefault();
                if (outdated == null) throw new LogicException("User not found");

                if (updated.Address != null) outdated.Address = updated.Address;
                if (updated.Password != null) outdated.Password = updated.Password;
                if (updated.Name != null) outdated.Name = updated.Name;

                return _userRepository.UpdateUser(outdated);

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
            return (User u) => guid == null || u.Id == guid;
        }
    }
}
