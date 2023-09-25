using ApiModels.In;
using Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiModelsTest.In
{
    [TestClass]
    public class UserRequestTest
    {
        private string nameSample = "nameSample";
        private string addressSample = "address sample";
        private Guid guidSample = Guid.NewGuid();
        private string emailSample = "email@sample.com";
        private string passwordSample = "passwordSample";
        private List<string> roles = new List<string>();
        private UserRequest request;


        [TestInitialize]
        public void Init()
        {
            request = new UserRequest()
            {
                Name = nameSample,
                Address = addressSample,
                Guid = guidSample,
                Email = emailSample,
                Password = passwordSample,
                Roles = roles,
            };

        }


        [TestMethod]
        public void AssignsProperties()
        {
            Assert.AreEqual(request.Name, nameSample);
            Assert.AreEqual(request.Address, addressSample);
            Assert.AreEqual(request.Guid, guidSample);
            Assert.AreEqual(request.Email, emailSample);
            Assert.AreEqual(request.Password, passwordSample);
            Assert.IsTrue(request.Roles.Count == 0);
        }

        [TestMethod]
        public void UserRequestToUserEntity() {

            User entity = request.ToEntity();

            Assert.AreEqual(entity.Name, nameSample);
            Assert.AreEqual(entity.Address, addressSample);
            Assert.AreEqual(entity.Guid, guidSample);
            Assert.AreEqual(entity.Email, emailSample);
            Assert.AreEqual(entity.Password, passwordSample);
            Assert.IsTrue(entity.Roles.Count == 0);

        }
    }
}
