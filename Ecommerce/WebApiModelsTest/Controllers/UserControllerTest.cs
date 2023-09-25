using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
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
            logic.Setup(logic => logic.GetAllUsers(null)).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsers(null);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.First().Name, expectedMappedResult.First().Name);
            Assert.AreEqual(resultValue.First().Address, expectedMappedResult.First().Address);
            Assert.AreEqual(resultValue.First().Email, expectedMappedResult.First().Email);
        }

        [TestMethod]
        public void GetExistingUserById()
        {
            Guid guid = Guid.NewGuid();

            IEnumerable<User> expected = new List<User>()
            {
                new User {
                    Email= "mail1@sample.com",
                    Name="name1",
                    Password="password1",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                    Guid = guid,
                },
            };

            var expectedMappedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User,bool>>())).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsers(null);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.First().Name, expectedMappedResult.First().Name);
            Assert.AreEqual(resultValue.First().Address, expectedMappedResult.First().Address);
            Assert.AreEqual(resultValue.First().Email, expectedMappedResult.First().Email);
            Assert.AreEqual(resultValue.First().Password, expectedMappedResult.First().Password);
            Assert.AreEqual(resultValue.First().Roles, expectedMappedResult.First().Roles);
        }

        [TestMethod]
        public void GetNonExistingUserByIdReturnsEmptyList()
        {
            IEnumerable<User> expected = new List<User>() { };

            var expectedMappedResult = expected.ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsers(Guid.NewGuid());

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue.Count, 0);
        }


        [TestMethod]
        public void CreateUser()
        {
            CreateUserRequest received = new CreateUserRequest()
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
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.AddUser(It.IsAny<User>())).Returns(expected);
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
        }

        [TestMethod]
        public void DeleteUser()
        {
            Guid guid = Guid.NewGuid();
            CreateUserRequest received = new CreateUserRequest()
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
            Guid = guid,
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User,bool>?>())).Returns(new List<User> { expected });
            logic.Setup(logic => logic.DeleteUser(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.DeleteUser(guid);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
        }

        [TestMethod]
        public void UpdateUser()
        {
            UpdateUserRequest received = new UpdateUserRequest()
            {
                Name = "nameSample",
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
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.UpdateUser(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.UpdateUser(received,guid);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Roles, expectedMappedResult.Roles);
        }

    }
}
