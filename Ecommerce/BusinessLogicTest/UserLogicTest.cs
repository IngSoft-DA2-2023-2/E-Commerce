using BusinessLogic;
using DataAccessInterface;
using DataAccessInterface.Exceptions;
using Domain;
using LogicInterface.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTest
{
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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.AddUserByAdmin(expected);

            repo.VerifyAll();

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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.AddUserByThemself(expected);

            repo.VerifyAll();

            Assert.AreEqual(result.Name, expected.Name);
            Assert.AreEqual(result.Email, expected.Email);
            Assert.AreEqual(result.Address, expected.Address);
            Assert.AreEqual(result.Password, expected.Password);
            Assert.AreEqual(result.Roles.Count, 1);
            Assert.AreEqual(result.Roles[0], "buyer");
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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> {expected});
            var userLogic = new UserLogic(repo.Object);

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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { expected });
            var userLogic = new UserLogic(repo.Object);

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
                Roles = new List<string> { "buyer" },
 
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Throws(new DataAccessException());
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { });
            var userLogic = new UserLogic(repo.Object);

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
                Roles = new List<string> { "buyer" },

            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Throws(new DataAccessException());
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { });
            var userLogic = new UserLogic(repo.Object);

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
               Roles = new List<string> { "buyer" },
               },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userLogic = new UserLogic(repo.Object);

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
               Roles = new List<string> { "buyer" },
               },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.GetAllUsers(u=>u.Name=="Juan");

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
               Roles = new List<string> { "buyer" },
               },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException());
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.GetAllUsers(u => u.Name == "Juan");
        }

        [TestMethod]
        public void UpdateUserByAdmin()
        {

            User modifications = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
                Password= "12345",
                Email= "a@a.com",
                Roles = new List<string> { }
            };

            User outdated = new User()
            {
                Name = "Juancito",
                Email = "a@a.com",
                Address = "aaa2",
                Password = "123456",
                Roles = new List<string> { "buyer" },
            };
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(modifications);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User,bool>>())).Returns(new List<User> { outdated});

            var userLogic = new UserLogic(repo.Object);

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

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User,bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object);
            
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
            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(modifications);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User> { outdated });

            var userLogic = new UserLogic(repo.Object);

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

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object);

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

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException());
            var userLogic = new UserLogic(repo.Object);

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

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Throws(new DataAccessException());
            var userLogic = new UserLogic(repo.Object);

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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.DeleteUser(It.IsAny<User>())).Returns(deleted);
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.DeleteUser(toDelete);

            repo.VerifyAll();
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
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.DeleteUser(It.IsAny<User>())).Throws(new DataAccessException());
            var userLogic = new UserLogic(repo.Object);

           userLogic.DeleteUser(user);
        }

    }
}


