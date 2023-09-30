using BusinessLogic;
using DataAccessInterface;
using Domain;
using LogicInterface.Exceptions;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class SessionLogicTest
    {
        private readonly Guid userGuid = Guid.NewGuid();
        private readonly string emailSample = "test@test.com";
        private readonly string passwordSample = "passwordSample";
        private User userSample;

        [TestInitialize]
        public void Init()
        {
             userSample = new User()
            {
                Email = emailSample,
                Password = passwordSample,
                Id = userGuid,
            };
        }

        [TestMethod]
        public void CreateNewSession()
        {
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = userSample,
            };

            Mock<IUserRepository> repoUser = new Mock<IUserRepository>(MockBehavior.Strict);
            Mock<ISessionRepository> repoSession = new Mock<ISessionRepository>(MockBehavior.Strict);

            repoUser.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { userSample });
            repoSession.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            var sessionLogic = new SessionLogic(repoUser.Object,repoSession.Object);

            var result = sessionLogic.LogIn(emailSample, passwordSample);

            repoUser.VerifyAll();
            repoSession.VerifyAll();

            Assert.AreEqual(result.User, userSample);
            Assert.AreEqual(result.User.Id, userGuid);
            Assert.AreEqual(result.Id, session.Id);
        }


        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateSessionWithInvalidCredentialsThrowsLogicalException()
        {
            User user = new User()
            {
                Email = emailSample,
                Password = passwordSample,
                Id = userGuid,
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = userSample,
            };

            Mock<IUserRepository> repoUser = new Mock<IUserRepository>(MockBehavior.Strict);
            Mock<ISessionRepository> repoSession = new Mock<ISessionRepository>(MockBehavior.Strict);

            repoUser.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());

            var sessionLogic = new SessionLogic(repoUser.Object, repoSession.Object);

            var result = sessionLogic.LogIn(emailSample, passwordSample);
        }

        [TestMethod]
        public void DeleteExistingSession()
        {

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = userSample,
            };

            Mock<ISessionRepository> repo = new Mock<ISessionRepository>(MockBehavior.Strict);

            repo.Setup(logic => logic.DeleteSession(It.IsAny<Session>())).Returns(session);
            repo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session,bool>>())).Returns(new List<Session> { session});

            var sessionLogic = new SessionLogic(null,repo.Object);

            var result = sessionLogic.LogOut(userGuid);

            repo.VerifyAll();

            Assert.AreEqual(result.User, userSample);
            Assert.AreEqual(result.User.Id, userGuid);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void DeleteNonExistingSessionThrowsException()
        {

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = userSample,
            };

            Mock<ISessionRepository> repo = new Mock<ISessionRepository>(MockBehavior.Strict);

            repo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session>());

            var sessionLogic = new SessionLogic(null, repo.Object);

            var result = sessionLogic.LogOut(userGuid);
        }
    }
}
