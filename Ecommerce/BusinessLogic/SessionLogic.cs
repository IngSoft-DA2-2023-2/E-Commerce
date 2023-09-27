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
            User user = _userRepository.GetAllUsers(u =>
                                                    u.Email == email
                                                    && u.Password == password
                                                    ).FirstOrDefault();

            if (user is null) throw new LogicException("Incorrect credentials");

            Session? sessionForUser = _sessionRepository.GetSessions(s => s.UserId == user.Guid).FirstOrDefault();

            if (sessionForUser is not null) return sessionForUser;

            Session newSession = new Session
            {
                SessionToken = Guid.NewGuid(),
                UserId = user.Guid
            };

            return _sessionRepository.CreateSession(newSession);
        }
    }
}
