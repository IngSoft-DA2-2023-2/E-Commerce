using BusinessLogic;
using DataAccessInterface;
using Domain;
using LogicInterface.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class UserLogicTest
    {

        [TestMethod]
        public void CreateUserCorrect()
        {
            User expected = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Email = "a@a.com",
                Address = "aaa",
                Password = "12345",
                Roles = new List<string> { "buyer" },
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            repo.Setup(logic => logic.Exist(It.IsAny<Func<User, bool>>())).Returns(false);
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.CreateUser(expected);

            repo.VerifyAll();

            Assert.AreEqual(result.Id, expected.Id);
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
            repo.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(newUser);
            repo.Setup(logic => logic.Exist(It.IsAny<Func<User, bool>>())).Returns(true);

            var userLogic = new UserLogic(repo.Object);

            Assert.ThrowsException<LogicException>(() => userLogic.CreateUser(newUser));
        }

        [TestMethod]
        public void GetAllUsers()
        {
            IEnumerable<User> expected = new List<User>()
            {
               new User()
               {
               Id = Guid.NewGuid(),
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

            var result = userLogic.GetAllUsers("");

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

           User modifications =  new User()
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



            Mock <IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(updated);
            var userLogic = new UserLogic(repo.Object);

            var result = userLogic.UpdateUser(modifications);

            repo.VerifyAll();
            Assert.AreEqual(result.Name, updated.Name);
            Assert.AreEqual(result.Address, updated.Address);
        }

    }
}


