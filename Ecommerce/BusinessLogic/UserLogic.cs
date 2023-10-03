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
        private readonly ISessionRepository _sessionRepository;
        public UserLogic(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            this._sessionRepository = sessionRepository;
            this._userRepository = userRepository;
        }

        public User AddUserByAdmin(User user)
        {
            try
            {
                if (_userRepository.GetAllUsers(u => u.Email == user.Email).Any())
                {
                    throw new LogicException("Existing user with that email");
                }
                user.Id = Guid.NewGuid();
                return _userRepository.CreateUser(user);
            }
            catch (DataAccessException e)
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
                user.Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } };
                user.Id = Guid.NewGuid();
                return _userRepository.CreateUser(user);
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public IEnumerable<User> GetAllUsers(Func<User, bool>? predicate)
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

                return _userRepository.UpdateUser(updated);
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

                return _userRepository.UpdateUser(updated);
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }

        public User DeleteUser(Guid userId)
        {
            try
            {
                return _userRepository.DeleteUser(userId);
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

        public bool IsAdmin(string token)
        {
            try
            {
                Guid tokenGuid = Guid.Parse(token);
                var sessions = _sessionRepository.GetSessions(s => s.Id == tokenGuid);
                if (sessions.Count() == 0) return false;
                return sessions.FirstOrDefault().
                User.Roles.
                Contains(new StringWrapper() { Info = "admin" });
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
            catch { return false; }

        }

        public Guid GetUserIdFromToken(string userHeader)
        {
            Guid tokenGuid = Guid.Parse(userHeader);
            return _sessionRepository.GetSessions(s => s.Id == tokenGuid).FirstOrDefault().User.Id;

        }



        public bool IsBuyer(string token)
        {
            try
            {
                Guid tokenGuid = Guid.Parse(token);
                var sessions = _sessionRepository.GetSessions(s => s.Id == tokenGuid);
                if (sessions.Count() == 0) return false;
                return sessions.FirstOrDefault().
                     User.Roles.
                     Contains(new StringWrapper() { Info = "buyer" });
            }
            catch (DataAccessException e)
            {
                throw new LogicException(e);
            }
        }
    }
}
