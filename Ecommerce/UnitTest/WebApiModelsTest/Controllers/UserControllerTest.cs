using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
            List<User> usersSample = new()
            {
                new User {Email= "mail1@sample.com",Name="name1",Password="password1" },
                new User {Email= "mail2@sample.com",Name="name2",Password="password2" },
                new User {Email= "mail3@sample.com",Name="name3",Password="password3" },
            };

            Mock<IUserLogic> mock = new();
            mock.Setup(u => u.GetUsers()).Returns(usersSample);

            UserController userController = new(mock.Object);
            var result = userController.GetAllUsers().Result as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(usersSample, result.Value);
        }

        [TestMethod]
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

    }
}
