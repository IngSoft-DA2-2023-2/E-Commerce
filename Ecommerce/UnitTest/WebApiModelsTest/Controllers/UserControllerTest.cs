using ApiModels;
using Domain;
using LogicInterface;
using LogicInterface.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using WebApi.Models.In;
using WebApi.Models.Out;

namespace UnitTest.WebApiModelsTest.Controllers
{
    [TestClass]
    public class UserControllerTest
    {

        [TestMethod]
        public void GetAllUsersOk()
        {
            IEnumerable<User> expected = new List<User>()
            {
                new User {
                    Email= "mail1@sample.com",
                    Name="name1",
                    Password="password1",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                },
            };
            
            var expectedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetUsers()).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedResult);

            var result = userController.GetAllUsers();

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode,expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.First().Name, expectedResult.First().Name);
            Assert.AreEqual(resultValue.First().Address, expectedResult.First().Address);
            Assert.AreEqual(resultValue.First().Email, expectedResult.First().Email);
            Assert.AreEqual(resultValue.First().Id, expectedResult.First().Id);
        }

       /* [TestMethod]
        public void GetAllUsersException()
        {
            List<User> usersSample = new()
            {
                new User {Email= "mail1@sample.com",Name="name1",Password="password1" },
                new User {Email= "mail2@sample.com",Name="name2",Password="password2" },
                new User {Email= "mail3@sample.com",Name="name3",Password="password3" },
            };

            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.GetUsers()).Throws(new Exception());

            UserController userController = new(mock.Object);
            var result = userController.GetAllUsers().Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public void CreateUserOK()
        {
            CreateUserRequest userRequest = new CreateUserRequest()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };
            User user = new User()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };


            Guid guid = Guid.NewGuid();
            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.AddUser(It.Is<User>(user => user.Name == userRequest.Name && user.Email == userRequest.Email &&
                    user.Roles.Equals(userRequest.Roles) && user.Address == userRequest.Address && user.Password == userRequest.Password

            ))).Returns(guid);

            UserController userController = new(mock.Object);
            var result = userController.CreateUser(userRequest).Result as OkObjectResult;

            Assert.IsNotNull(result);
            var response = result.Value as CreateUserResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(userRequest.Name, response.Name);
            Assert.AreEqual(userRequest.Email, response.Email);
            Assert.AreEqual(userRequest.Roles, response.Roles);
            Assert.AreEqual(userRequest.Password, response.Password);
            Assert.AreEqual(userRequest.Address, response.Address);
        }

        [TestMethod]
        public void CreateUserThrowsException()
        {
            CreateUserRequest userRequest = new()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };
            User user = new()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };

            Guid guid = Guid.NewGuid();
            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.AddUser(It.Is<User>(user => user.Name == userRequest.Name && user.Email == userRequest.Email &&
                    user.Roles.Equals(userRequest.Roles) && user.Address == userRequest.Address && user.Password == userRequest.Password
            ))).Throws(new Exception());

            UserController userController = new(mock.Object);
            var result = userController.CreateUser(userRequest).Result as StatusCodeResult;

            Assert.AreEqual(500, result.StatusCode);
        }

        [TestMethod]
        public void CreateUserThrowsLogicException()
        {
            CreateUserRequest userRequest = new()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };
            User user = new()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
            };

            Guid guid = Guid.NewGuid();
            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.AddUser(It.Is<User>(user => user.Name == userRequest.Name && user.Email == userRequest.Email &&
                    user.Roles.Equals(userRequest.Roles) && user.Address == userRequest.Address && user.Password == userRequest.Password
            ))).Throws(new LogicException("error"));

            UserController userController = new(mock.Object);
            var result = userController.CreateUser(userRequest).Result as StatusCodeResult;

            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUserReturnsOk()
        {
            Mock<IUserLogic>mock = new();

            var userId = Guid.NewGuid();
            mock.Setup(logic => logic.DeleteUser(userId));

            UserController userController = new(mock.Object);
            var result = userController.DeleteUser(userId).Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUserThrowsLogicalException()
        {
            Mock<IUserLogic> mock = new();

            var userId = Guid.NewGuid();
            mock.Setup(logic => logic.DeleteUser(userId)).Throws(new LogicException("error"));

            UserController userController = new(mock.Object);
            var result = userController.DeleteUser(userId).Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }

        [TestMethod]
        public void DeleteUserThrowsException()
        {
            Mock<IUserLogic> mock = new();

            var userId = Guid.NewGuid();
            mock.Setup(logic => logic.DeleteUser(userId)).Throws(new Exception());

            UserController userController = new(mock.Object);
            var result = userController.DeleteUser(userId).Result as StatusCodeResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(500, result.StatusCode);
        }*/
    }
}
