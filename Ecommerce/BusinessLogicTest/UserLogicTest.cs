﻿using BusinessLogic;
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
        [ExpectedException(typeof(LogicException))]
        public void CreateUserWithExistingEmailThrowsLogicException()
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

            var result = userLogic.AddUser(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void CreateUserReceivesDataAccessExceptionAndThrowsLogicException()
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

            var result = userLogic.AddUser(expected);
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
        public void UpdateUser()
        {

            User modifications = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
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

            var result = userLogic.UpdateUser(modifications);

            repo.VerifyAll();
            Assert.AreEqual(result.Name, modifications.Name);
            Assert.AreEqual(result.Address, modifications.Address);
        }

        [TestMethod]
        [ExpectedException(typeof(LogicException))]
        public void UpdateUserThrowsLogicException()
        {
            User user = new User()
            {
                Name = "Juancito",
                Address = "aaa2",
            };

            Mock<IUserRepository> repo = new Mock<IUserRepository>(MockBehavior.Strict);
            repo.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User,bool>>())).Returns(new List<User>());
            var userLogic = new UserLogic(repo.Object);
            
            userLogic.UpdateUser(user);
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

