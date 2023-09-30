using ApiModels.In;
using ApiModels.Out;
using Domain;
using Domain.ProductParts;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApiModelsTest.Controller
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
                    Roles= new List < StringWrapper > { new StringWrapper() { Info = "buyer" } },
                },
            };
            var token = "testToken";


            var expectedMappedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers(null)).Returns(expected);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsers(token);

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
                    Roles=new List < StringWrapper > { new StringWrapper() { Info = "buyer" } },
                    Id = guid,
                },
            };

            var token = "testToken";

            var expectedMappedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsers(token);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.First().Name, expectedMappedResult.First().Name);
            Assert.AreEqual(resultValue.First().Address, expectedMappedResult.First().Address);
            Assert.AreEqual(resultValue.First().Email, expectedMappedResult.First().Email);
            Assert.AreEqual(resultValue.First().Roles, expectedMappedResult.First().Roles);
        }

        [TestMethod]
        public void GetNonExistingUserByIdReturnsEmptyList()
        {
            IEnumerable<User> expected = new List<User>() { };
            var token = "testToken";
            var expectedMappedResult = expected.ToList();
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            logic.Setup(logic => logic.GetAllUsers(It.IsAny<Func<User, bool>>())).Returns(expected);
            var userController = new UserController(logic.Object);
            OkObjectResult expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.GetUsersById(Guid.NewGuid(), token);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            List<UserResponse> resultValue = resultObject.Value as List<UserResponse>;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(resultValue.Count, 0);
        }


        [TestMethod]
        public void CreateUserByAdmin()
        {
            CreateUserByAdminRequest received = new CreateUserByAdminRequest()
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
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "role sample" } },
                Address = "address sample",
                Password = "password sample",
            };
            var token = "testToken";

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.AddUserByAdmin(It.IsAny<User>())).Returns(expected);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateUser", "User", new { id = 5 }, expectedMappedResult);

            var result = userController.RegistrationByAdmin(received, token);

            logic.VerifyAll();
            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
        }

        [TestMethod]
        public void CreateUserByThemself()
        {
            CreateUserByThemselfRequest received = new CreateUserByThemselfRequest()
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
                Address = "address sample",
                Password = "password sample",
            };

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.AddUserByThemself(It.IsAny<User>())).Returns(expected);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateUser", "User", new { id = 5 }, expectedMappedResult);

            var result = userController.SelfRegistration(received);

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
            CreateUserByAdminRequest received = new CreateUserByAdminRequest()
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
            Roles = new List<StringWrapper> { new StringWrapper() { Info = "role sample" } },
            Address = "address sample",
            Password = "password sample",
            Id = guid,
            };
            var token = "testToken";

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.DeleteUser(It.IsAny<Guid>())).Returns(expected);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.DeleteUser(guid, token);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Email, expectedMappedResult.Email);
        }

        [TestMethod]
        public void UpdateUserByAdmin()
        {
            UpdateUserRequestByAdmin received = new UpdateUserRequestByAdmin()
            {
                Name = "nameSample",
                Address = "address sample",
                Password = "password sample",
                Roles = new List<string>(),

            };

            Guid guid = Guid.NewGuid();

            User expected = new User()
            {
                Name = "nameSample",
                Email = "email@sample.com",
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "role sample" } },
                Address = "address sample",
                Password = "password sample",
            };
            var token = "testToken";

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.UpdateUserByAdmin(It.IsAny<User>())).Returns(expected);
            logic.Setup(logic => logic.IsAdmin(It.Is<string>(s => s == token))).Returns(true);
            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.UpdateUserByAdmin(received, guid, token);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Roles.First(), expectedMappedResult.Roles.First());
        }

        [TestMethod]
        public void UpdateUserByThemself()
        {
            UpdateUserRequestByThemself received = new UpdateUserRequestByThemself()
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
                Roles = new List<StringWrapper> { new StringWrapper() { Info = "role sample" } },
                Address = "address sample",
                Password = "password sample",
                Id = guid
            };
            var token = "testToken";

            var expectedMappedResult = new UserResponse(expected);
            Mock<IUserLogic> logic = new Mock<IUserLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.UpdateUserByThemself(It.Is<User>(u => u.Guid == guid))).Returns(expected);
            logic.Setup(logic => logic.GetUserIdFromToken(It.Is<string>(s => s == token))).Returns(guid);

            var userController = new UserController(logic.Object);
            var expectedObjectResult = new OkObjectResult(expectedMappedResult);

            var result = userController.UpdateUserByThemself(received, token);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            UserResponse resultValue = resultObject.Value as UserResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);

            Assert.AreEqual(resultValue.Name, expectedMappedResult.Name);
            Assert.AreEqual(resultValue.Address, expectedMappedResult.Address);
            Assert.AreEqual(resultValue.Roles.First(), expectedMappedResult.Roles.First());
            Assert.AreEqual(resultValue.Guid, guid);
        }

    }
}
