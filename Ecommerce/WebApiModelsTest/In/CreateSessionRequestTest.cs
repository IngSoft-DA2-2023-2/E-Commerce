using ApiModels.In;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.In
{
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
