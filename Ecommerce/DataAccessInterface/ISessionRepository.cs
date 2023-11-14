using Domain;

namespace DataAccessInterface
{
    public interface ISessionRepository
    {
        Session CreateSession(Session session);
        Session DeleteSession(Session session);
        IEnumerable<Session> GetSessions(Func<Session, bool> pred);
    }
}