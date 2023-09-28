using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterface
{
    public interface ISessionRepository
    {
        Session CreateSession(Session session);
        Session DeleteSession(Session session);
        IEnumerable<Session> GetSessions(Func<Session, bool> pred);
    }
}
