﻿using BusinessLogic;
using DataAccessInterface;
using Domain;
using LogicInterface.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    public class SessionLogicTest
    {
        private readonly Guid userGuid = Guid.NewGuid();
        private readonly string emailSample = "test@test.com";
        private readonly string passwordSample = "passwordSample";

        [TestMethod]
        public void CreateNewSession()
        {

            User user = new User()
            {
                Email = emailSample,
                Password = passwordSample,
                Guid = userGuid,
            };

            Session session = new Session()
            {
                SessionToken = Guid.NewGuid(),
                UserId = userGuid,
            };

            Mock<IUserRepository> repoUser = new Mock<IUserRepository>(MockBehavior.Strict);
            Mock<ISessionRepository> repoSession = new Mock<ISessionRepository>(MockBehavior.Strict);

            repoUser.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            repoSession.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            repoSession.Setup(logic => logic.GetSessions(It.IsAny<Func<Session,bool>>())).Returns(new List<Session>());


            var sessionLogic = new SessionLogic(repoUser.Object,repoSession.Object);

            var result = sessionLogic.LogIn(emailSample, passwordSample);

            repoUser.VerifyAll();
            repoSession.VerifyAll();

            Assert.AreEqual(result.UserId, userGuid);
            Assert.AreEqual(result.SessionToken, session.SessionToken);
        }

        [TestMethod]
        public void CreateSessionReturnsExistingOne()
        {

            User user = new User()
            {
                Email = emailSample,
                Password = passwordSample,
                Guid = userGuid,
            };

            Session session = new Session()
            {
                SessionToken = Guid.NewGuid(),
                UserId = userGuid,
            };

            Mock<IUserRepository> repoUser = new Mock<IUserRepository>(MockBehavior.Strict);
            Mock<ISessionRepository> repoSession = new Mock<ISessionRepository>(MockBehavior.Strict);

            repoUser.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            repoSession.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session});

            var sessionLogic = new SessionLogic(repoUser.Object, repoSession.Object);

            var result = sessionLogic.LogIn(emailSample, passwordSample);

            repoUser.VerifyAll();
            repoSession.VerifyAll();

            Assert.AreEqual(result.UserId, userGuid);
            Assert.AreEqual(result.SessionToken, session.SessionToken);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateSessionWithInvalidCredentialsThrowsLogicalException()
        {

            User user = new User()
            {
                Email = emailSample,
                Password = passwordSample,
                Guid = userGuid,
            };

            Session session = new Session()
            {
                SessionToken = Guid.NewGuid(),
                UserId = userGuid,
            };

            Mock<IUserRepository> repoUser = new Mock<IUserRepository>(MockBehavior.Strict);
            Mock<ISessionRepository> repoSession = new Mock<ISessionRepository>(MockBehavior.Strict);

            repoUser.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());

            var sessionLogic = new SessionLogic(repoUser.Object, repoSession.Object);

            var result = sessionLogic.LogIn(emailSample, passwordSample);
        }
    }
}