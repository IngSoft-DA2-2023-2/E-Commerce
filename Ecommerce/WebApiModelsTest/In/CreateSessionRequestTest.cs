using ApiModels.In;
using System.Diagnostics.CodeAnalysis;

namespace WebApiModelsTest.In
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class CreateSessionRequestTest
    {
        private string emailSample = "test@email.com";
        private string passwordSample = "password";
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
