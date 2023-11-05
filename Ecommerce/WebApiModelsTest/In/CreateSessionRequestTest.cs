using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateSessionRequestTest
    {
        private readonly string emailSample = "test@email.com";
        private readonly string passwordSample = "password";
        [TestMethod]
        public void AssignsProperties()
        {
            CreateSessionRequest req = new CreateSessionRequest
            {
                Email = emailSample,
                Password = passwordSample
            };

            Assert.AreEqual(req.Email, emailSample);
            Assert.AreEqual(req.Password, passwordSample);
        }
    }
}
