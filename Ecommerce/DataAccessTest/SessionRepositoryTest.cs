using DataAccess.Context;
using DataAccess.Repository;
using DataAccessInterface;
using Domain;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessTest
{
    [TestClass]
    public class SessionRepositoryTest
    {
        [TestMethod]
        public void CreateSession()
        {
            Session session = new Session() { 
                SessionToken= Guid.NewGuid(),
                UserId=Guid.NewGuid() };
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
            Session sessionSample = new Session { SessionToken = Guid.NewGuid() , UserId = Guid.NewGuid() };

            var sessionContext = new Mock<ECommerceContext>();
            sessionContext.Setup(c => c.Sessions).ReturnsDbSet(new List<Session> { sessionSample });

            ISessionRepository sessionRepository = new SessionRepository(sessionContext.Object);
            var expectedReturn = sessionRepository.GetSessions(s=>true).ToList();

            Assert.AreEqual(expectedReturn.Count, 1);
            Assert.AreEqual(expectedReturn[0], sessionSample);
        }

        [TestMethod]
        public void DeleteSession()
        {
            Session session = new Session()
            {
                SessionToken = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            };
            var userContext = new Mock<ECommerceContext>();
            userContext.Setup(s => s.Sessions).ReturnsDbSet(new List<Session> { session});
            userContext.Setup(c => c.Sessions.Remove(session));
            userContext.Setup(c => c.SaveChanges());

            ISessionRepository sessionRepository = new SessionRepository(userContext.Object);
            var expectedReturn = sessionRepository.DeleteSession(session);
            Assert.AreEqual(expectedReturn, session);
        }
    }
}
