using ApiModels;
using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;

namespace WebApiModelsTest.Controller
{
    [TestClass]
    public class SessionControllerTest
    {
        [TestMethod]
        public void CreateSession()
        {
            CreateSessionRequest received = new CreateSessionRequest()
            {
                Email = "email@sample.com",
                Password = "password",
            };

            Guid guid = Guid.NewGuid();
            Session session = new Session() { SessionToken = guid };
            var expectedMappedResult = new SessionResponse(session);

            IEnumerable<User> expected = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Roles=new List<string>{"buyer"},
                },
            };
            var token = "testToken";


            var userExpectedMappedResult = expected.Select(u => new UserResponse(u)).ToList();
            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(expected);




            Mock<ISessionLogic> logic = new Mock<ISessionLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.LogIn(It.IsAny<string>(), It.IsAny<string>())).Returns(session);
            var sessionController = new SessionController(logic.Object, userLogic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateSession", "Session", new { id = 5 }, expectedMappedResult);

            var result = sessionController.LogIn(received);

            logic.VerifyAll();
            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            SessionResponse resultValue = resultObject.Value as SessionResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(guid, resultValue.Token);
        }

        [TestMethod]
        public void DeleteSession()
        {
            Guid guid = Guid.NewGuid();

            DeleteSessionRequest received = new DeleteSessionRequest()
            {
                Token = guid,
            };


            IEnumerable<User> expected = new List<User>()
            {
                new User {
                    Email= "email@sample.com",
                    Name="name1",
                    Password="password",
                    Address="address sample",
                    Guid= Guid.NewGuid(),
                    Roles =new List<string>{"buyer"},
                },
            };
            Session session = new Session() { SessionToken = guid, User = expected.First() };
            var expectedMappedResult = new SessionResponse(session);

            Mock<IUserLogic> userLogic = new Mock<IUserLogic>(MockBehavior.Strict);
            userLogic.Setup(logic => logic.GetAllUsers(null)).Returns(expected);
            userLogic.Setup(logic => logic.GetUserIdFromToken(It.IsAny<string>())).Returns(expected.First().Guid);


            Mock<ISessionLogic> logic = new Mock<ISessionLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.LogOut(It.IsAny<Guid>())).Returns(session);
            var sessionController = new SessionController(logic.Object, userLogic.Object);

            var result = sessionController.LogOut(received, guid.ToString());

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            SessionResponse resultValue = resultObject.Value as SessionResponse;

            Assert.AreEqual(resultObject.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(guid, resultValue.Token);
        }
    }
}
