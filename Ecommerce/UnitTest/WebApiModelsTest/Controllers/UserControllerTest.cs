using ApiModels;
using ApiModels.UserRequest;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;

namespace UnitTest.WebApiModelsTest.Controllers
{
    [TestClass]
    public class UserControllerTest
    {

        [TestMethod]
        public void GetAllUsers()
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

            var expectedMappedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers("")).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetAllUsers();

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.First().Name, expectedMappedResult.First().Name);
            Assert.AreEqual(resultValue.First().Address, expectedMappedResult.First().Address);
            Assert.AreEqual(resultValue.First().Email, expectedMappedResult.First().Email);
            Assert.AreEqual(resultValue.First().Id, expectedMappedResult.First().Id);
        }

        [TestMethod]
        public void CreateUser()
        {
            UserRequest received = new UserRequest()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Address = "address sample",
                Password = "password sample",

            };

            User expected = new User()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
                Id = Guid.NewGuid(),
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.CreateUser(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateUser", "User", new { id = 5 }, expectedMappedResult);

            var result = userController.CreateUser(received);

            logic.VerifyAll();
            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
            Assert.AreEqual(resultValue.Id, expectedMappedResult.Id);
        }

        [TestMethod]
        public void DeleteUser()
        {
            UserRequest received = new UserRequest()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Address = "address sample",
                Password = "password sample",

            };

            User expected = new User()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
                Id = Guid.NewGuid(),
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.DeleteUser(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.DeleteUser(received);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
            Assert.AreEqual(resultValue.Id, expectedMappedResult.Id);
        }

        [TestMethod]
        public void UpdateUser()
        {
            UserRequest received = new UserRequest()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Address = "address sample",
                Password = "password sample",

            };

            Guid guid = Guid.NewGuid();

            User expected = new User()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<string> { "role sample" },
                Address = "address sample",
                Password = "password sample",
                Id = guid,
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.UpdateUser(received);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
            Assert.AreEqual(resultValue.Id, expectedMappedResult.Id);
            Assert.AreEqual(resultValue.Roles, expectedMappedResult.Roles);
        }





    }
}
