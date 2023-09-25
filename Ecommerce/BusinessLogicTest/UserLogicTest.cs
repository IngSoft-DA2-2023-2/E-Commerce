using BusinessLogic;
using DataAccessInterface;
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
        public void CreateUserCorrect()
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

            var result = userLogic.AddUser(expected);

            repo.VerifyAll();

            Assert.AreEqual(result.Name, expected.Name);
            Assert.AreEqual(result.Email, expected.Email);
            Assert.AreEqual(result.Address, expected.Address);
            Assert.AreEqual(result.Password, expected.Password);
            Assert.AreEqual(result.Roles, expected.Roles);
        }

        [TestMethod]
        public void CreateUserLogicException()
        {
            User newUser = new User();

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Throws(new LogicException());
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(new List<User>());

            var userLogic = new UserLogic(repo.Object);

            Assert.ThrowsException<LogicException>(() => userLogic.AddUser(newUser));
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
        public void UpdateUser()
        {

            User modifications = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };

            User updated = new User()
            {
                Name = "Juancito",
                Email = "a@a.com",
                Address = "aaa2",
                Password = "123456",
                Roles = new List<string> { "buyer" },
            };



            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(updated);
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.UpdateUser(modifications);

            repo.VerifyAll();
            Assert.AreEqual(result.Name, updated.Name);
            Assert.AreEqual(result.Address, updated.Address);
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

    }
}


