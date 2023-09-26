using ApiModels.In;
using ApiModels.Out;
using Domain;
using LogicInterface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;

namespace WebApiModelsTest.Controllers
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
            Session session = new Session() { Token = guid };
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
