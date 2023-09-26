using DataAccessInterface;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;

        public SessionLogic(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public Session LogIn(string email, string password)
        {
            IEnumerable<User> correctCredentials = _userRepository.GetAllUsers(u => 
                                                                          u.Email == email 
                                                                          && u.Password == password
                                                                          );
            if (!correctCredentials.Any()) throw new LogicException("Incorrect credentials");

            Session session = new Session
            {
                SessionToken = Guid.NewGuid(),
                UserId = correctCredentials.First().Guid
            };

            return _sessionRepository.CreateSession(session);
        }
    }
}
