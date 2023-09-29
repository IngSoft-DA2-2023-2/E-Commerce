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
            Session session = new Session() { Id = guid };
            var expectedMappedResult = new SessionResponse(session);

            Mock<ISessionLogic> logic = new Mock<ISessionLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.LogIn(It.IsAny<string>(),It.IsAny<string>())).Returns(session);
            var sessionController = new SessionController(logic.Object);
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

            Session session = new Session() { Id = guid };
            var expectedMappedResult = new SessionResponse(session);

            Mock<ISessionLogic> logic = new Mock<ISessionLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.LogOut(It.IsAny<Guid>())).Returns(session);
            var sessionController = new SessionController(logic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateSession", "Session", new { id = 5 }, expectedMappedResult);

            var result = sessionController.LogOut(received);

            logic.VerifyAll();
            OkObjectResult resultObject = result as OkObjectResult;
            SessionResponse resultValue = resultObject.Value as SessionResponse;

            Assert.AreEqual(resultObject.StatusCode, StatusCodes.Status200OK);
            Assert.AreEqual(guid, resultValue.Token);
        }
    }
}
