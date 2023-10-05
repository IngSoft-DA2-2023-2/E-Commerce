using DataAccess.Context;
using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ECommerceContext _eCommerceContext;
        public SessionRepository(ECommerceContext context)
        {
            _eCommerceContext = context;
        }

        public Session CreateSession(Session session)
        {
            _eCommerceContext.Sessions.Add(session);
            _eCommerceContext.SaveChanges();
            return session;
        }

        public Session DeleteSession(Session session)
        {
            _eCommerceContext.Sessions.Remove(session);
            _eCommerceContext.SaveChanges();
            return session;
        }

        public IEnumerable<Session> GetSessions(Func<Session, bool> pred)
        {
            return _eCommerceContext.Sessions.
                Include(s => s.User).
                ThenInclude(u => u.Roles).
                Where(pred).
                ToList();
        }
    }
}
