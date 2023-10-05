using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using Domain.ProductParts;
using LogicInterface.Exceptions;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class UserLogicTest
    {

        [TestMethod]
        public void CreateUserByAdminCorrect()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);


            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByAdmin(expected);

            userRepo.VerifyAll();

            Assert.AreEqual(result.Name, expected.Name);
            Assert.AreEqual(result.Email, expected.Email);
            Assert.AreEqual(result.Address, expected.Address);
            Assert.AreEqual(result.Password, expected.Password);
            Assert.AreEqual(result.Roles, expected.Roles);
        }

        [TestMethod]
        public void CreateUserByThemselfCorrect()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByThemself(expected);

            repo.VerifyAll();

            Assert.AreEqual(result.Name, expected.Name);
            Assert.AreEqual(result.Email, expected.Email);
            Assert.AreEqual(result.Address, expected.Address);
            Assert.AreEqual(result.Password, expected.Password);
            Assert.AreEqual(result.Roles.Count, 1);
            Assert.AreEqual(result.Roles.First().Info, "buyer");
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateUserByAdminWithExistingEmailThrowsLogicException()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { expected });
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByAdmin(expected);
        }


        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateUserByThemselfWithExistingEmailThrowsLogicException()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { expected });
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByThemself(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateUserByAdminReceivesDataAccessExceptionAndThrowsLogicException()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },

            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Throws(new DataAccessException($"User with email a@a.com already exists."));
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { });
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByAdmin(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateUserByThemselfReceivesDataAccessExceptionAndThrowsLogicException()
        {
            User expected = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },

            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Throws(new DataAccessException($"User with email a@a.com already exists."));
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { });
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.AddUserByThemself(expected);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            IEnumerable<User> expected = new List<User>()
            {
               new User()
               {
               Name = "Juan",
               Email = "a@a.com",
               Address = "aaa",
               Password = "12345",
               Roles = new List < StringWrapper > { new StringWrapper() { Info = "buyer" } },
               },
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected.First(),
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.GetAllUsers(null);

            repo.VerifyAll();
            Assert.AreEqual(result.First().Name, expected.First().Name);
            Assert.AreEqual(result.First().Address, expected.First().Address);
            Assert.AreEqual(result.First().Password, expected.First().Password);
            Assert.AreEqual(result.First().Email, expected.First().Email);
            Assert.AreEqual(result.First().Roles, expected.First().Roles);
        }

        [TestMethod]
        public void GetUserByPredicate()
        {
            IEnumerable<User> expected = new List<User>()
            {
               new User()
               {
               Name = "Juan",
               Email = "a@a.com",
               Address = "aaa",
               Password = "12345",
               Roles = new List < StringWrapper > { new StringWrapper() { Info = "buyer" } },
               },
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected.First(),
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.GetAllUsers(u => u.Name == "Juan");

            repo.VerifyAll();
            Assert.AreEqual(result.First().Name, expected.First().Name);
            Assert.AreEqual(result.First().Address, expected.First().Address);
            Assert.AreEqual(result.First().Password, expected.First().Password);
            Assert.AreEqual(result.First().Email, expected.First().Email);
            Assert.AreEqual(result.First().Roles, expected.First().Roles);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void GetUsersThrowsDataAccessException()
        {
            IEnumerable<User> expected = new List<User>()
            {
               new User()
               {
               Name = "Juan",
               Email = "a@a.com",
               Address = "aaa",
               Password = "12345",
               Roles = new List < StringWrapper > ()
               },
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = expected.First(),
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException($"User with email a@a.com already exists."));
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.GetAllUsers(u => u.Name == "Juan");
        }

        [TestMethod]
        public void UpdateUserByAdmin()
        {

            User modifications = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
                Password = "12345",
                Email = "a@a.com",
                Roles = new List<StringWrapper>(),
            };

            User outdated = new User()
            {
                Name = "Juancito",
                Email = "a@a.com",
                Address = "aaa2",
                Password = "123456",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = modifications,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(modifications);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { outdated });

            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.UpdateUserByAdmin(modifications);

            repo.VerifyAll();
            Assert.AreEqual(result.Name, modifications.Name);
            Assert.AreEqual(result.Address, modifications.Address);
            Assert.AreEqual(result.Password, modifications.Password);
            Assert.AreEqual(result.Email, modifications.Email);
            Assert.AreEqual(result.Roles.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void UpdateUserByAdminThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            userLogic.UpdateUserByAdmin(user);
        }

        [TestMethod]
        public void UpdateUserByThemself()
        {

            User modifications = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
                Password = "12345",
            };

            User outdated = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
                Password = "123456",
            };

            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = modifications,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(modifications);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { outdated });

            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            var result = userLogic.UpdateUserByThemself(modifications);

            repo.VerifyAll();
            Assert.AreEqual(result.Name, modifications.Name);
            Assert.AreEqual(result.Address, modifications.Address);
            Assert.AreEqual(result.Password, modifications.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void UpdateUserByUserThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            userLogic.UpdateUserByThemself(user);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void UpdateUserByAdminHandlesDataAccessExceptionAndThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException("empty"));
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            userLogic.UpdateUserByAdmin(user);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void UpdateUserByThemselfHandlesDataAccessExceptionAndThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException("empty"));
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            userLogic.UpdateUserByThemself(user);
        }

        [TestMethod]
        public void DeleteUser()
        {
            User toDelete = new User() { Email = "a@a.com" };

            User deleted = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = toDelete,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.DeleteUser(It.IsAny<Guid>())).Returns(deleted);
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            var result = userLogic.DeleteUser(toDelete.Id);

            userRepo.VerifyAll();
            Assert.AreEqual(result.Email, toDelete.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void DeleteUserThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.DeleteUser(It.IsAny<Guid>())).Throws(new DataAccessException("empty"));
            var userLogic = new UserLogic(repo.Object, sessionRepo.Object);

            userLogic.DeleteUser(user.Id);
        }

        [TestMethod]
        public void IsAdminReturnsTrue()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "admin" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            sessionRepo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session });

            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            Assert.IsTrue(userLogic.IsAdmin(session.Id.ToString()));
        }
        [TestMethod]
        public void IsBuyerReturnsTrue()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "buyer" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            sessionRepo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session });

            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            Assert.IsTrue(userLogic.IsBuyer(session.Id.ToString()));
        }

        [TestMethod]
        public void IsAdminReturnsFalse()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "user" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            sessionRepo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session });

            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            Assert.AreEqual(userLogic.IsAdmin(session.Id.ToString()), false);
        }
        [TestMethod]
        public void IsBuyerReturnsFalse()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "user" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            sessionRepo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session });

            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            Assert.AreEqual(userLogic.IsBuyer(session.Id.ToString()), false);
        }

        [TestMethod]
        public void GetUserIdFromTokenRight()
        {
            User user = new User()
            {
                Name = "Juan",
                Email = "juan@gmail.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "user" } },
                Id = Guid.NewGuid(),
            };
            Session session = new Session()
            {
                Id = Guid.NewGuid(),
                User = user,
            };

            Mock<ISessionRepository> sessionRepo = new Mock<ISessionRepository>(MockBehavior.Strict);
            sessionRepo.Setup(logic => logic.CreateSession(It.IsAny<Session>())).Returns(session);
            sessionRepo.Setup(logic => logic.GetSessions(It.IsAny<Func<Session, bool>>())).Returns(new List<Session> { session });

            Mock<IUserRepository> userRepo = new Mock<IUserRepository>(MockBehavior.Strict);
            userRepo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { user });
            var userLogic = new UserLogic(userRepo.Object, sessionRepo.Object);

            Assert.AreEqual(userLogic.GetUserIdFromToken(session.Id.ToString()), user.Id);
        }

    }
}


