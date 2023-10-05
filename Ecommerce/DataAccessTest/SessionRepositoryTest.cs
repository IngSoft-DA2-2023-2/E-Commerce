using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace DataAccessTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class SessionRepositoryTest
    {
        [TestMethod]
        public void CreateSession()
        {
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = new User(),
            };
            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(s => s.Sessions).ReturnsDbSet(new List<Session>());
            userContext.Setup(c => c.Sessions.Add(session));
            userContext.Setup(c => c.SaveChanges());

            ISessionRepository sessionRepository = new SessionRepository(userContext.Object);
            var expectedReturn = sessionRepository.CreateSession(session);
            Assert.AreEqual(expectedReturn, session);
        }

        [TestMethod]
        public void GetAllSessions()
        {
            Session sessionSample = new Session { Id = Guid.NewGuid(), User = new User() };

            var sessionContext = new Mock<ECommerceContext>();
            sessionContext.Setup(c => c.Sessions).ReturnsDbSet(new List<Session> { sessionSample });

            ISessionRepository sessionRepository = new SessionRepository(sessionContext.Object);
            var expectedReturn = sessionRepository.GetSessions(s => true).ToList();

            Assert.AreEqual(expectedReturn.Count, 1);
            Assert.AreEqual(expectedReturn[0], sessionSample);
        }

        [TestMethod]
        public void DeleteSession()
        {
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = new User()
            };
            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(s => s.Sessions).ReturnsDbSet(new List<Session> { session });
            userContext.Setup(c => c.Sessions.Remove(session));
            userContext.Setup(c => c.SaveChanges());

            ISessionRepository sessionRepository = new SessionRepository(userContext.Object);
            var expectedReturn = sessionRepository.DeleteSession(session);
            Assert.AreEqual(expectedReturn, session);
        }
    }
}
