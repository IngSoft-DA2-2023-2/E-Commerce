using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
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
            var expectedMappedResult = new CreateSessionResponse(session);

            Mock<ISessionLogic> logic = new Mock<ISessionLogic>(MockBehavior.Strict);
            logic.Setup(logic => logic.LogIn(It.IsAny<string>(),It.IsAny<string>())).Returns(session);
            var sessionController = new SessionController(logic.Object);
            var expectedObjectResult = new CreatedAtActionResult("CreateSession", "Session", new { id = 5 }, expectedMappedResult);

            var result = sessionController.LogIn(received);

            logic.VerifyAll();
            CreatedAtActionResult resultObject = result as CreatedAtActionResult;
            CreateSessionResponse resultValue = resultObject.Value as CreateSessionResponse;

            Assert.AreEqual(resultObject.StatusCode, expectedObjectResult.StatusCode);
            Assert.AreEqual(guid, resultValue.Token);
        }
    }
}
