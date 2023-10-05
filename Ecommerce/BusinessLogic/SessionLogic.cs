using DataAccessInterface;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;

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
            User user = _userRepository.GetAllUsers(u => u.Email == email && u.Password == password).FirstOrDefault();

            if (user is null) throw new LogicException("Incorrect credentials");

            Session newSession = new Session
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            return _sessionRepository.CreateSession(newSession);
        }

        public Session LogOut(Guid token)
        {
            Session? session = _sessionRepository.GetSessions(s => s.Id == token).FirstOrDefault();

            if (session is null) throw new LogicException("Invalid token");

            _sessionRepository.DeleteSession(session);

            return session;
        }
    }
}
